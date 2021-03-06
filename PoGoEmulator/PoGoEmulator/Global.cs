﻿using PoGoEmulator.Models;
using System;
using System.Collections.Generic;
using System.Net;
using POGOProtos.Networking.Responses;

namespace PoGoEmulator
{
    public static class Global
    {
        /// <summary>
        /// server configs 
        /// </summary>
        public static Configs Cfg = new Configs()
        {
#if DEBUG
            RequestTimeout = new TimeSpan(0, 10, 0)
#endif
        };

        /// <summary>
        /// static datas 
        /// </summary>
        public static Dictionary<string, KeyValuePair<byte[], GetAssetDigestResponse>> GameAssets =
            new Dictionary<string, KeyValuePair<byte[], GetAssetDigestResponse>>();

        /// <summary>
        /// static datas 
        /// </summary>
        public static GameMaster GameMaster = null;

        public static Dictionary<string, string> DefaultResponseHeader { get; set; } = new Dictionary<string, string>()
        {
            {"Accept-Encoding", "gzip"},
            {"Content-Type", "application/x-www-form-urlencoded" }
        };
    }
}