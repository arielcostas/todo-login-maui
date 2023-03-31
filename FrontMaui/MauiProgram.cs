﻿using Microsoft.Extensions.Logging;

namespace FrontMaui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}

#nullable enable
public static class Sesion
{
    public static string? Token { get; set; }
}