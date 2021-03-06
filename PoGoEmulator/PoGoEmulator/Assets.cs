﻿using PoGoEmulator.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using PoGoEmulator.Models;
using POGOProtos.Networking.Responses;

namespace PoGoEmulator
{
    public static class Assets
    {
        public static string[] Plaforms = new string[] { "android", "ios" };

        public static void ValidateAssets()
        {
            Logger.Write("Validating Assets..");

            if (!File.Exists(Path.Combine(Global.Cfg.DUMP_ASSET_PATH, "game_master")))
                throw new Exception("'game_master' not found");

#if DEBUG
            Logger.Write("Pokemons are loading from 'asset_digest'..", Enums.LogLevel.Debug);
#endif
            var max = Global.Cfg.MAX_POKEMON_NATIONAL_ID;
            var limit = Plaforms.Length;

            for (int i = 0; i < limit; i++)
            {
                var platform = Plaforms[i];
                var path_platform = Path.Combine(Global.Cfg.DUMP_ASSET_PATH, platform);
                var path_asset_digest = Path.Combine(path_platform, "asset_digest");
                if (!File.Exists(path_asset_digest))
                    throw new Exception("'asset_digest' not found");
                else
                {
                    BinaryReader sr = new BinaryReader(new StreamReader(path_asset_digest).BaseStream);
                    byte[] buffer = sr.ReadBytes((int)sr.BaseStream.Length);
                    Global.GameAssets[platform] =
                        new KeyValuePair<byte[], GetAssetDigestResponse>(buffer, buffer.Proton<GetAssetDigestResponse>(false));
                }

                for (var j = 1; j <= max; j++)
                {
                    var modelFile = "pm" + (j >= 10 ? j >= 100 ? "0" : "00" : "000") + j;
                    var path = Path.Combine(path_platform, modelFile);
                    if (!File.Exists(path))
                        throw new Exception($"{path} not found");
                }
            }
            Logger.Write("Pokemons are successfully loaded...");
        }
    }
}