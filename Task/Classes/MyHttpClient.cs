﻿namespace Task.Classes
{
    class MyHttpClient : IMyHttpClient
    {
        public async System.Threading.Tasks.Task<byte[]> WriteBytesFromResource(string resource)
        {
            using var client = new HttpClient();
            var output = await client.GetByteArrayAsync(resource);
            return output;
        }
    }
}