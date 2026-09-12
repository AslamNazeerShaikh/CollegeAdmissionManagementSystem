---
name: csharp-development
description: Modern C# .NET development practices and patterns for Uno Platform cross-platform apps
version: 1.0.0
author: opencode
tags:
  - csharp
  - dotnet
  - uno-platform
  - maui
  - nativeaot
  - mvvm
  - communitytoolkit
capabilities:
  - idiomatic-csharp
  - mvvm-communitytoolkit
  - dependency-injection
  - async-patterns
  - records-pattern-matching
  - source-generators
  - nativeaot-compatibility
  - uno-platform-patterns
  - cross-platform-architecture
references:
  - "https://learn.microsoft.com/dotnet/csharp/"
  - "https://platform.uno/docs/"
  - "https://github.com/CommunityToolkit/dotnet"
  - "https://learn.microsoft.com/dotnet/core/deploying/native-aot/"
  - "https://github.com/unoplatform/Uno.Themes"
  - "https://github.com/unoplatform/uno/tree/master/src/Uno.WinUI.Lottie"
examples:
  - name: "Records for Immutable Models"
    description: "Use records for DTOs and domain models with pattern matching"
    code: |
      // Core/Models/Course.cs
      public record Course(
          string Id,
          string Name,
          string Degree,
          string Details,
          string Fee,
          string Eligibility,
          IReadOnlyList<string> PdfFiles
      );
      
      // Pattern matching switch expression
      public string GetCourseDisplayName(Course course) => course switch
      {
          { Degree: "B.Sc." } => $"🎓 {course.Name} (Bachelor of Science)",
          { Degree: "M.Sc." } => $"🎓 {course.Name} (Master of Science)",
          { Degree: "BCA" } => $"💻 {course.Name} (Bachelor of Computer Applications)",
          { Degree: "MCA" } => $"💻 {course.Name} (Master of Computer Applications)",
          { Degree: "BBA" } => $"📊 {course.Name} (Bachelor of Business Administration)",
          { Degree: "MBA" } => $"📊 {course.Name} (Master of Business Administration)",
          _ => course.Name
      };

  - name: "MVVM with CommunityToolkit.Mvvm"
    description: "ObservableObject, ObservableProperty, and RelayCommand for clean ViewModels"
    code: |
      // Core/ViewModels/CoursesViewModel.cs
      using CommunityToolkit.Mvvm.ComponentModel;
      using CommunityToolkit.Mvvm.Input;
      using CollegeAdmission.Core.Models;
      using CollegeAdmission.Core.Services;
      
      public partial class CoursesViewModel : ObservableObject
      {
          private readonly ICourseService _courseService;
          private readonly INavigationService _navigationService;
          
          [ObservableProperty]
          private ObservableCollection<Course> _courses = new();
          
          [ObservableProperty]
          [NotifyCanExecuteChangedFor(nameof(SelectCourseCommand))]
          private Course? _selectedCourse;
          
          public CoursesViewModel(ICourseService courseService, INavigationService navigationService)
          {
              _courseService = courseService;
              _navigationService = navigationService;
          }
          
          [RelayCommand(CanExecute = nameof(CanSelectCourse))]
          private async Task SelectCourseAsync()
          {
              if (SelectedCourse is not null)
              {
                  await _navigationService.NavigateToCourseDetailAsync(SelectedCourse);
              }
          }
          
          private bool CanSelectCourse() => SelectedCourse is not null;
          
          [RelayCommand]
          private async Task LoadCoursesAsync()
          {
              var courses = await _courseService.GetCoursesAsync();
              Courses.Clear();
              foreach (var course in courses)
              {
                  Courses.Add(course);
              }
          }
      }

  - name: "Dependency Injection with Platform-Specific Implementations"
    description: "Register platform-specific services via DI in each platform head"
    code: |
      // Core/Infrastructure/ServiceCollectionExtensions.cs
      using Microsoft.Extensions.DependencyInjection;
      using CollegeAdmission.Core.Services;
      
      public static class ServiceCollectionExtensions
      {
          public static IServiceCollection AddCoreServices(this IServiceCollection services)
          {
              services.AddSingleton<ICourseService, CourseService>();
              services.AddSingleton<IRegistrationService, RegistrationService>();
              services.AddSingleton<IThemeService, ThemeService>();
              services.AddSingleton<INavigationService, NavigationService>();
              
              // AI Services
              services.AddSingleton<IVoiceService, PlatformVoiceService>();
              services.AddSingleton<ITextAiService, McpTextAiService>();
              services.AddSingleton<IVisionService, PlatformVisionService>();
              services.AddSingleton<ILocalModelService, OnnxRuntimeModelService>();
              services.AddSingleton<IAiOrchestrator, AiOrchestrator>();
              services.AddSingleton<IMcpClient, HttpMcpClient>();
              
              return services;
          }
          
          public static IServiceCollection AddPlatformServices(this IServiceCollection services)
          {
              // Platform-specific implementations registered in each platform head
              // iOS: services.AddSingleton<ISqliteService, IosSqliteService>();
              // Android: services.AddSingleton<ISqliteService, AndroidSqliteService>();
              // Desktop: services.AddSingleton<ISqliteService, DesktopSqliteService>();
              return services;
          }
      }
      
      // Platforms/iOS/Program.cs
      builder.Services.AddCoreServices();
      builder.Services.AddSingleton<ISqliteService, IosSqliteService>();
      builder.Services.AddSingleton<IVoiceService, IosVoiceService>();
      builder.Services.AddSingleton<IVisionService, IosVisionService>();

  - name: "NativeAOT-Compatible Code Patterns"
    description: "Avoid reflection, use source generators, annotate for trimming"
    code: |
      // Use System.Text.Json source generation for AOT
      // Core/Services/Json/SerializationContext.cs
      using System.Text.Json.Serialization;
      
      [JsonSerializable(typeof(Course))]
      [JsonSerializable(typeof(RegistrationData))]
      [JsonSerializable(typeof(List<Course>))]
      [JsonSerializable(typeof(UserPreferences))]
      internal partial class AppJsonContext : JsonSerializerContext { }
      
      // Usage in services
      public class CourseService : ICourseService
      {
          private static readonly JsonSerializerOptions Options = new(AppJsonContext.Default)
          {
              PropertyNameCaseInsensitive = true
          };
          
          public async Task<List<Course>> GetCoursesAsync()
          {
              var json = await File.ReadAllTextAsync("Assets/Data/courses.json");
              return JsonSerializer.Deserialize<List<Course>>(json, Options) ?? new();
          }
      }
      
      // Annotate for trimming
      [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | 
                                   DynamicallyAccessedMemberTypes.PublicProperties)]
      public record RegistrationData(...);

  - name: "Cross-Platform SQLite with Abstraction"
    description: "Platform-specific SQLite providers behind common interface"
    code: |
      // Core/Services/ISqliteService.cs
      public interface ISqliteService
      {
          Task InitializeAsync(CancellationToken ct = default);
          Task<T?> GetAsync<T>(string sql, object? parameters = null);
          Task<List<T>> QueryAsync<T>(string sql, object? parameters = null);
          Task<int> ExecuteAsync(string sql, object? parameters = null);
          Task<int> ExecuteScalarAsync<T>(string sql, object? parameters = null);
          Task<IDbTransaction> BeginTransactionAsync();
          void EnableWriteAheadLogging();
          void EnableForeignKeys();
      }
      
      // Platforms/iOS/PlatformSpecific/IosSqliteProvider.cs
      using SQLite; // praeclarum/sqlite-net
      
      public class IosSqliteService : ISqliteService
      {
          private SQLiteAsyncConnection? _connection;
          
          public async Task InitializeAsync(CancellationToken ct = default)
          {
              var path = GetDatabasePath("admission.db3");
              _connection = new SQLiteAsyncConnection(path, 
                  SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex);
              
              _connection.EnableWriteAheadLogging();
              await CreateTablesAsync();
          }
          
          private string GetDatabasePath(string filename)
          {
              var docs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
              var lib = Path.Combine(docs, "..", "Library");
              return Path.Combine(lib, filename);
          }
      }
      
      // Platforms/Android/PlatformSpecific/AndroidSqliteProvider.cs
      using Microsoft.Data.Sqlite;
      
      public class AndroidSqliteService : ISqliteService
      {
          private SqliteConnection? _connection;
          
          public async Task InitializeAsync(CancellationToken ct = default)
          {
              var path = Path.Combine(FileSystem.AppDataDirectory, "admission.db3");
              _connection = new SqliteConnection($"Data Source={path};Mode=ReadWriteCreate;Cache=Shared");
              await _connection.OpenAsync(ct);
              
              using var cmd = _connection.CreateCommand();
              cmd.CommandText = "PRAGMA journal_mode=WAL; PRAGMA foreign_keys=ON;";
              await cmd.ExecuteNonQueryAsync(ct);
              
              await CreateTablesAsync(ct);
          }
      }

  - name: "Uno Platform Theme Integration"
    description: "Material theming with Dark/Light/High Contrast support"
    code: |
      // UI/Resources/Themes/ThemeResources.xaml
      <ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                          xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
          <!-- Light Theme Overrides -->
          <Color x:Key="PrimaryColor">#6200EE</Color>
          <Color x:Key="SecondaryColor">#03DAC5</Color>
          <Color x:Key="TertiaryColor">#FF9800</Color>
          <Color x:Key="ErrorColor">#F44336</Color>
          <Color x:Key="SurfaceColor">#FFFFFF</Color>
          <Color x:Key="OnSurfaceColor">#131F2B</Color>
          <Color x:Key="OutlineColor">#D6DFE8</Color>
          
          <!-- Dark Theme Overrides (in MaterialDarkTheme.xaml) -->
          <!-- <Color x:Key="SurfaceColor">#111B29</Color> -->
          <!-- <Color x:Key="OnSurfaceColor">#E8EEF5</Color> -->
          <!-- <Color x:Key="OutlineColor">#26374C</Color> -->
      </ResourceDictionary>
      
      // Core/Services/ThemeService.cs
      public partial class ThemeService : ObservableObject, IThemeService
      {
          [ObservableProperty]
          private ThemeMode _currentTheme = ThemeMode.System;
          
          public event Action<ThemeMode>? ThemeChanged;
          
          public void SetTheme(ThemeMode mode)
          {
              CurrentTheme = mode;
              ApplyTheme(mode);
              ThemeChanged?.Invoke(mode);
              SavePreference(mode);
          }
          
          private void ApplyTheme(ThemeMode mode)
          {
              var themeDict = Application.Current.Resources.MergedDictionaries
                  .OfType<ResourceDictionary>()
                  .FirstOrDefault(d => d.Source.OriginalString.Contains("DynamicThemeOverlay"));
              
              if (themeDict != null)
              {
                  var newTheme = mode switch
                  {
                      ThemeMode.Light => new MaterialLightTheme(),
                      ThemeMode.Dark => new MaterialDarkTheme(),
                      _ => GetSystemTheme() == ThemeMode.Dark ? new MaterialDarkTheme() : new MaterialLightTheme()
                  };
                  
                  Application.Current.Resources.MergedDictionaries.Remove(themeDict);
                  Application.Current.Resources.MergedDictionaries.Add(newTheme);
              }
          }
      }

  - name: "Lottie Animation Integration"
    description: "Using Uno.WinUI.Lottie for cross-platform animations"
    code: |
      // UI/Resources/Styles/LottieStyles.xaml
      <ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                          xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                          xmlns:lottie="using:Uno.WinUI.Lottie">
          <Style TargetType="lottie:LottieVisualSource" x:Key="LottieStyle">
              <Setter Property="AutoPlay" Value="True" />
              <Setter Property="RepeatCount" Value="-1" />
          </Style>
      </ResourceDictionary>
      
      // UI/Views/SplashPage.xaml
      <Page xmlns:lottie="using:Uno.WinUI.Lottie">
          <Grid>
              <lottie:LottieView Source="ms-appx:///Assets/Lottie/tutorials_online.json"
                                 Style="{StaticResource LottieStyle}"
                                 Width="300" Height="300"
                                 HorizontalAlignment="Center"
                                 VerticalAlignment="Center" />
          </Grid>
      </Page>
      
      // Behaviors/AutoPlayLottieBehavior.cs
      public class AutoPlayLottieBehavior : Behavior<LottieView>
      {
          protected override void OnAttached()
          {
              base.OnAttached();
              AssociatedObject.Loaded += OnLoaded;
          }
          
          private void OnLoaded(object sender, RoutedEventArgs e)
          {
              AssociatedObject.Play();
          }
      }

  - name: "AI Service Integration (MCP Client)"
    description: "Model Context Protocol client for AI tool integration"
    code: |
      // Core/Services/IAiService.cs
      public interface ITextAiService
      {
          Task<string> CompleteAsync(string prompt, AiModel model, CancellationToken ct);
          Task<StructuredOutput<T>> ExtractAsync<T>(string text, JsonSchema schema, CancellationToken ct);
          Task<McpResponse> CallMcpAsync(McpRequest request, CancellationToken ct);
      }
      
      public interface IVoiceService
      {
          Task<string> SpeechToTextAsync(Stream audio, CancellationToken ct);
          Task<Stream> TextToSpeechAsync(string text, VoiceOptions options, CancellationToken ct);
          IAsyncEnumerable<PartialResult> StreamSpeechToTextAsync(Stream audio, CancellationToken ct);
      }
      
      // Core/Services/Mcp/HttpMcpClient.cs
      public class HttpMcpClient : IMcpClient
      {
          private readonly HttpClient _http;
          private readonly ILogger<HttpMcpClient> _log;
          
          public async Task<McpResponse> SendAsync(McpRequest request, CancellationToken ct)
          {
              var json = JsonSerializer.Serialize(request, McpJsonContext.Default.McpRequest);
              var content = new StringContent(json, Encoding.UTF8, "application/json");
              
              var response = await _http.PostAsync("/mcp", content, ct);
              response.EnsureSuccessStatusCode();
              
              var responseJson = await response.Content.ReadAsStringAsync(ct);
              return JsonSerializer.Deserialize<McpResponse>(responseJson, McpJsonContext.Default.McpResponse)!;
          }
      }
      
      // Usage in RegistrationViewModel
      [RelayCommand]
      private async Task FillFromVoiceAsync()
      {
          var voiceText = await _voiceService.SpeechToTextAsync(audioStream, ct);
          var result = await _aiOrchestrator.FillFormFromVoiceAsync(voiceText);
          ApplyRegistrationData(result);
      }