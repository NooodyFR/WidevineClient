using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using WidevineClient.Widevine;
using static WidevineClient.HttpUtil;
using static WidevineClient.Utils;

namespace WidevineClient
{
    class Tests
    {
        //https://shaka-player-demo.appspot.com/demo/#audiolang=zh-CN;textlang=zh-CN;uilang=zh-CN;asset=https://storage.googleapis.com/shaka-demo-assets/sintel-widevine/dash.mpd;adTagUri=https://pubads.g.doubleclick.net/gampad/ads?sz=640x480&iu=/124319096/external/ad_rule_samples&ciu_szs=300x250&ad_rule=1&impl=s&gdfp_req=1&env=vp&output=vmap&unviewed_position_start=1&cust_params=deployment%3Ddevsite%26sample_ar%3Dpremidpostpod&cmsid=496&vid=short_onecue&correlator=;panel=HOME;build=uncompiled
        public static void Test()
        {
            string initDataB64 = "AAAAp3Bzc2gAAAAA7e+LqXnWSs6jyCfc1R0h7QAAAIcSEFF0U4YtQlb9i61PWEIgBNcSEPCTfpp3yFXwptQ4ZMXZ82USEE1LDKJawVjwucGYPFF+4rUSEJAqBRprNlaurBkm/A9dkjISECZHD0KW1F0Eqbq7RC4WmAAaDXdpZGV2aW5lX3Rlc3QiFnNoYWthX2NlYzViZmY1ZGM0MGRkYzlI49yVmwY=";
            string licenseUrl = "https://cwip-shaka-proxy.appspot.com/no_auth";
            Logger.Cyan("get cert...");
            var resp1 = PostData(licenseUrl, null, new byte[] { 0x08, 0x04 });
            var certDataB64 = Convert.ToBase64String(resp1);
            Logger.Cyan("get challenge...");
            var cdm = new CDMApi();
            var challenge = cdm.GetChallenge(initDataB64, certDataB64, false, false);
            Logger.Cyan("get license...");
            var resp2 = PostData(licenseUrl, null, challenge);
            var licenseB64 = Convert.ToBase64String(resp2);
            cdm.ProvideLicense(licenseB64);
            Logger.Cyan("get Keys...");
            List<ContentKey> keys = cdm.GetKeys();
            foreach (var key in keys)
            {
                Logger.Print(key);
            }
        }

        //https://bitmovin.com/demos/drm
        public static void Test2()
        {
            string initDataB64 = "AAAAW3Bzc2gAAAAA7e+LqXnWSs6jyCfc1R0h7QAAADsIARIQ62dqu8s0Xpa7z2FmMPGj2hoNd2lkZXZpbmVfdGVzdCIQZmtqM2xqYVNkZmFsa3IzaioCSEQyAA==";
            string licenseUrl = "https://widevine-proxy.appspot.com/proxy";
            Logger.Cyan("get cert...");
            var resp1 = PostData(licenseUrl, null, new byte[] { 0x08, 0x04 });
            var certDataB64 = Convert.ToBase64String(resp1);
            Logger.Cyan("get challenge...");
            var cdm = new CDMApi();
            var challenge = cdm.GetChallenge(initDataB64, certDataB64, false, false);
            Logger.Cyan("get license...");
            var resp2 = PostData(licenseUrl, null, challenge);
            var licenseB64 = Convert.ToBase64String(resp2);
            cdm.ProvideLicense(licenseB64);
            Logger.Cyan("get Keys...");
            List<ContentKey> keys = cdm.GetKeys();
            foreach (var key in keys)
            {
                Logger.Print(key);
            }
        }

        //https://www.ezdrm.com/html/drm-demos.asp
        public static void Test3()
        {
            string initDataB64 = "AAAAdnBzc2gAAAAA7e+LqXnWSs6jyCfc1R0h7QAAAFYIARIQyf3iK27xRyW+04N07kAMLBoIbW92aWRvbmUiMnsia2lkIjoieWYzaUsyN3hSeVcrMDROMDdrQU1MQT09IiwidHJhY2tzIjpbIlNEIl19KgJTRA==";
            string licenseUrl = "https://widevine-dash.ezdrm.com/widevine-php/widevine-foreignkey.php?pX=B03B45";
            Logger.Cyan("get cert...");
            var resp1 = PostData(licenseUrl, null, new byte[] { 0x08, 0x04 });
            var certDataB64 = Convert.ToBase64String(resp1);
            Logger.Cyan("get challenge...");
            var cdm = new CDMApi();
            var challenge = cdm.GetChallenge(initDataB64, certDataB64, false, false);
            Logger.Cyan("get license...");
            var resp2 = PostData(licenseUrl, null, challenge);
            var licenseB64 = Convert.ToBase64String(resp2);
            cdm.ProvideLicense(licenseB64);
            Logger.Cyan("get Keys...");
            List<ContentKey> keys = cdm.GetKeys();
            foreach (var key in keys)
            {
                Logger.Print(key);
            }
        }

        //https://www.kktv.me/play/09000264010001
        public static void Test4()
        {
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJfcmFuZG9tIjoiYnVsdmExMHFsbDFjNzJ2YXFibzAiLCJhdWQiOiJra3R2LmNvbSIsImV4cCI6MTYwNTE5MTMwMCwiaWF0IjoxNjA1MTA0OTAwLCJpc3MiOiJLS1RWIiwicm9sZSI6Imd1ZXN0Iiwic3ViIjoiZ3Vlc3Q6S0tUVi1jbGllbnRzOjUxY2ZlZjlmLTU3NjctNDY0Ni05MWU3LWNiMDA1Y2I0OWExMCIsInR5cGUiOiJnZW5lcmFsIn0.pT3oGLpwJM_8C82rK9MmcpkYYuPVGWfCqbS4x5mapUw";
            var headers = new Dictionary<string, string>()
            {
                ["authorization"] = $"Bearer {token}"
            };
            Logger.Cyan("get playback_token & device_id...");
            var tokenUrl = "https://api.kktv.me/v3/playback_tokens";
            var jsonStr = JObject.FromObject(new
            {
                title_id = "09000264",
                episode_id = "09000264010001",
                medium = "AVOD"
            }).ToString(Formatting.None);
            var webSource = GetString(PostData(tokenUrl, headers, jsonStr));
            var playbackToken = JObject.Parse(webSource)["data"]["playback_token"].ToString();
            var deviceId = JObject.Parse(webSource)["data"]["device_id"].ToString();
            headers["x-custom-data"] = $"token_type=playback&token_value={token}&device_id={deviceId}&client_platform=Web";
            Logger.Print($"playback_token: {playbackToken}");
            Logger.Print($"device_id: {deviceId}");
            string initDataB64 = "AAAAQHBzc2gAAAAA7e+LqXnWSs6jyCfc1R0h7QAAACAIARIQovTxM9GiG2kARQL1K+lULxoEa2t0diIEa2t0dg==";
            string licenseUrl = "https://license.kktv.com.tw/";
            Logger.Cyan("get cert...");
            var resp1 = PostData(licenseUrl, headers, new byte[] { 0x08, 0x04 });
            var certDataB64 = Convert.ToBase64String(resp1);
            Logger.Cyan("get challenge...");
            var cdm = new CDMApi();
            var challenge = cdm.GetChallenge(initDataB64, certDataB64, false, false);
            Logger.Cyan("get license...");
            var resp2 = PostData(licenseUrl, headers, challenge);
            var licenseB64 = Convert.ToBase64String(resp2);
            cdm.ProvideLicense(licenseB64);
            Logger.Cyan("get Keys...");
            List<ContentKey> keys = cdm.GetKeys();
            foreach (var key in keys)
            {
                Logger.Print(key);
            }
        }
    }
}
