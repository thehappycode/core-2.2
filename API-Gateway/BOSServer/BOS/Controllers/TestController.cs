using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BOS.BUSINESS.Services;
using BOS.BUSINESS.Helper;
using Microsoft.Extensions.Options;
using BOS.DB.Entities;

namespace BOS.Controllers
{
    [Route("api/[controller]")]
    [Authorize, ApiController]
    public class TestController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApplicationSetting _applicationSetting;

        TestService _TestService = new TestService();
        public TestController(IHttpClientFactory httpClientFactory, ApplicationSetting applicationSetting)
        {
            _httpClientFactory = httpClientFactory;
            _applicationSetting = applicationSetting;
        }

        // GET /api/Test
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var _client = _httpClientFactory.CreateClient();
            var _urlBase = _applicationSetting.BaseUrlApiGateWay;
            var _upstreamPathTemplate = "/ECMServer/Test";
            var _requestUrl = _urlBase + _upstreamPathTemplate;
            var _accessToken = Request.Headers["Authorization"].ToString();
            var response = await _TestService.Get(_client, _requestUrl, _accessToken);
            return Ok(response);
        }

        // Post /api/Test/Post
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] _Entities data)
        {
            var _client = _httpClientFactory.CreateClient();
            var _urlBase = _applicationSetting.BaseUrlApiGateWay;
            var _upstreamPathTemplate = "/ECMServer/Test";
            var _requestUrl = _urlBase + _upstreamPathTemplate;
            var _accessToken = Request.Headers["Authorization"].ToString();
            var response = await _TestService.Post(_client, _requestUrl, _accessToken, data);
            return Ok(response);
        }

        // Put /api/Test
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] _Entities data)
        {
            var _client = _httpClientFactory.CreateClient();
            var _urlBase = _applicationSetting.BaseUrlApiGateWay;
            var _upstreamPathTemplate = $"/ECMServer/Test/{id}";
            var _requestUrl = _urlBase + _upstreamPathTemplate;
            var _accessToken = Request.Headers["Authorization"].ToString();
            var response = await _TestService.Put(_client, _requestUrl, _accessToken, data);
            return Ok(response);
        }

        // Delete /api/Test/Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var _client = _httpClientFactory.CreateClient();
            var _urlBase = _applicationSetting.BaseUrlApiGateWay;
            var _upstreamPathTemplate = $"/ECMServer/Test/{id}";
            var _requestUrl = _urlBase + _upstreamPathTemplate;
            var _accessToken = Request.Headers["Authorization"].ToString();
            var response = await _TestService.Delete(_client, _requestUrl, _accessToken);
            return Ok(response);
        }


        // GET: api/Test
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            dynamic response = null;
            response = _TestService.GetAll(); 
            return Ok(response);
        }
    }
}
