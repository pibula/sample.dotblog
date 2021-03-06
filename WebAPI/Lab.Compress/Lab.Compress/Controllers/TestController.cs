﻿using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using Faker;
using Lab.Compress.Filters;

namespace Lab.Compress.Controllers
{
    public class TestController : ApiController
    {
        [DeflateCompression]
        [HttpGet]
        public IHttpActionResult DeflateCompression(string id)
        {
            var content = CreateData(id);
            return new ResponseMessageResult(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content    = new StringContent(content, Encoding.UTF8)
            });
        }

        //public IHttpActionResult Post()
        //{
        //    var zipContent = this.Request.Content.ReadAsByteArrayAsync().Result;
        //    var unzipContent = Deflate.Decompress(zipContent);
        //    var result     = Encoding.UTF8.GetString(unzipContent);
        //    return new ResponseMessageResult(new HttpResponseMessage
        //    {
        //        StatusCode = HttpStatusCode.OK,
        //        Content    = new StringContent(result, Encoding.UTF8)
        //    });
        //}
        [DeflateDecompression]
        public IHttpActionResult DeflateDecompression()
        {
            var content = this.Request.Content.ReadAsByteArrayAsync().Result;
            var result  = Encoding.UTF8.GetString(content);
            return new ResponseMessageResult(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content    = new StringContent(result, Encoding.UTF8)
            });
        }

        [GZipCompression]
        [HttpGet]
        public IHttpActionResult GZipCompression(string id)
        {
            var content = CreateData(id);
            return new ResponseMessageResult(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content    = new StringContent(content, Encoding.UTF8)
            });
        }

        [GZipDecompression]
        public IHttpActionResult GZipDecompression()
        {
            var content = this.Request.Content.ReadAsByteArrayAsync().Result;
            var result  = Encoding.UTF8.GetString(content);
            return new ResponseMessageResult(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content    = new StringContent(result, Encoding.UTF8)
            });
        }

        private static string CreateData(string id)
        {
            var builder = new StringBuilder();
            var times   = 0;

            for (var i = 0; i < 1000; i++)
            {
                builder.AppendLine($"{(i + 1).ToString("0000")},您好,"   +
                                   $"我是 {Name.FullName()},"            +
                                   $"今年 {RandomNumber.Next(1, 100)}歲," +
                                   $"家裡住在 {Address.Country()},{Address.City()}");
                times++;
            }

            builder.AppendLine($"共出現:{times}次");
            builder.AppendLine($"目前時間:{DateTime.Now.ToLocalTime()}");
            builder.AppendLine($"Id:{id}");
            return builder.ToString();
        }
    }
}