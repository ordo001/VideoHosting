using System.Diagnostics;
using VideoHostingApi.Common.Entities.Video;
using VideoHostingApi.Common.Repositories.Contracts;
using VideoHostingApi.VideoHandler.Services.Contracts;
using VideoHostingApi.VideoHandler.Services.Contracts.Models;

namespace VideoHostingApi.VideoHandler.Services;

/// <summary>
/// Сервис по обработке видео
/// </summary>
public class VideoProcessingService(IObjectStorageRepository<VideoFile> videoObjectStorageRepository) : IVideoProcessingService
{
    private static readonly string ffmpegPath = Path.Combine(AppContext.BaseDirectory, "FFmpeg", "ffmpeg.exe");
    public async Task ProcessVideoAsync(Guid videoId, CancellationToken cancellationToken)
    {
        var path = $"{videoId}/master";
        var stream = await videoObjectStorageRepository.DownloadFile(path, cancellationToken);
        
        var result = await ProcessAsync(stream.FileStream, cancellationToken);
        
        foreach (var file in result.Files.AsEnumerable())
        {
            await videoObjectStorageRepository.UploadFile($"{videoId}/{file.Key}",file.Value,"application/vnd.apple.mpegurl", cancellationToken);
        }
    }

    private async Task<HlsResult> ProcessAsync(Stream inputStream, CancellationToken cancellationToken)
    {
        var tempDir = Path.Combine(Path.GetTempPath(), "hls_" + Guid.NewGuid());

        Directory.CreateDirectory(tempDir);
        
        var inputFile = Path.Combine(tempDir, "input.mp4");
        await using (var file = File.Create(inputFile))
        {
            await inputStream.CopyToAsync(file, cancellationToken);
        }

        var profiles = new List<(string Folder, string Resolution, string Bitrate)>
        {
            ("1080p", "1920x1080", "5000k"),
            ("720p", "1280x720", "3000k"),
            ("480p", "854x480", "1500k"),
        };
            
        foreach (var (folder, res, bitrate) in profiles)
        {
            var outputDir = Path.Combine(tempDir, folder);
            Directory.CreateDirectory(outputDir);

            var args =
                $"-i \"{inputFile}\" " +
                $"-vf scale={res} -b:v {bitrate} -c:a aac -ac 2 -b:a 128k " +
                "-hls_time 4 -hls_playlist_type vod " +
                $"-hls_segment_filename \"{outputDir}/segment_%03d.ts\" " +
                $"{outputDir}/index.m3u8";

            await RunFfmpeg(args, cancellationToken);
        }
        
        
        var masterPlaylistPath = Path.Combine(tempDir, "master.m3u8");

        await File.WriteAllTextAsync(masterPlaylistPath,
            """
            #EXTM3U
            #EXT-X-VERSION:3

            #EXT-X-STREAM-INF:BANDWIDTH=5000000,RESOLUTION=1920x1080
            1080p/index.m3u8

            #EXT-X-STREAM-INF:BANDWIDTH=3000000,RESOLUTION=1280x720
            720p/index.m3u8

            #EXT-X-STREAM-INF:BANDWIDTH=1500000,RESOLUTION=854x480
            480p/index.m3u8
            """, cancellationToken);
        
        var result = new HlsResult();

        foreach (var file in Directory.GetFiles(tempDir, "*.m3u8", SearchOption.AllDirectories))
        {
            var relative = Path.GetRelativePath(tempDir, file).Replace("\\", "/");
            result.Files[relative] = await FileToStreamAsync(file);
        }

        foreach (var ts in Directory.GetFiles(tempDir, "*.ts", SearchOption.AllDirectories))
        {
            var relative = Path.GetRelativePath(tempDir, ts).Replace("\\", "/");
            result.Files[relative] = await FileToStreamAsync(ts);
        }
        
        return  result;
    }
    
    private async Task RunFfmpeg(string args, CancellationToken ct)
    {
        if (!File.Exists(ffmpegPath))
            throw new FileNotFoundException($"FFmpeg not found: {ffmpegPath}");

        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = ffmpegPath,
                Arguments = args,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            }
        };

        process.Start();
        
        var stdErrTask = process.StandardError.ReadToEndAsync(ct);
        await process.WaitForExitAsync(ct);
        var stderr = await stdErrTask;

        if (process.ExitCode != 0)
            throw new Exception($"FFmpeg error ({process.ExitCode}):\n{stderr}");
    }

    private async Task<MemoryStream> FileToStreamAsync(string path)
    {
        var ms = new MemoryStream();
        await using var fs = File.OpenRead(path);
        await fs.CopyToAsync(ms);
        ms.Position = 0;
        return ms;
    }
}