namespace TempStick;

using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Threading;
using System = System;

public partial class TempStickClient 
{
    private string _baseUrl = "https://tempstickapi.com/api/v1";
    private HttpClient _httpClient;
    private Lazy<JsonSerializerOptions> _options;

    public TempStickClient(HttpClient httpClient)
    {
        if (httpClient == null)
            throw new ArgumentNullException(nameof(httpClient));

        if (!httpClient.DefaultRequestHeaders.Contains("X-API-KEY"))
            throw new ArgumentNullException("An API key in the X-API-KEY default request headers was not found in the HttpClient.");

        _httpClient = httpClient;
        _options = new Lazy<JsonSerializerOptions>(CreateSerializerSettings);
    }

    public TempStickClient(string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
            throw new ArgumentNullException(nameof(apiKey));

        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
        _options = new Lazy<JsonSerializerOptions>(CreateSerializerSettings);
    }

    private JsonSerializerOptions CreateSerializerSettings()
    {
        var settings = new JsonSerializerOptions();
        UpdateJsonSerializerSettings(settings);
        return settings;
    }

    public string BaseUrl
    {
        get { return _baseUrl; }
        set { _baseUrl = value; }
    }

    protected JsonSerializerOptions JsonSerializerOptions { get { return _options.Value; } }

    partial void UpdateJsonSerializerSettings(JsonSerializerOptions settings);

    partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url);
    partial void PrepareRequest(HttpClient client, HttpRequestMessage request, System.Text.StringBuilder urlBuilder);
    partial void ProcessResponse(HttpClient client, HttpResponseMessage response);

    /// <summary>
    /// Get sensor readings
    /// </summary>
    /// <returns>Successful response</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    /// <param name="setting">The time period. Available values: today, 24_hours, yesterday, last_week, 7_days, this_week, this_month, 30_days, last_month, three_months, custom</param>
    /// <param name="start">The start date in YYYY-MM-DD format if custom is used</param>
    /// <param name="end">The end date in YYYY-MM-DD format if custom is used</param>
    public virtual Task<ReadingResponse> GetReadingsAsync(string sensor_id)
    {
        return GetReadingsAsync(sensor_id, null, "today", null, null, CancellationToken.None);
    }

    /// <summary>
    /// Get sensor readings
    /// </summary>
    /// <returns>Successful response</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    /// <param name="setting">The time period. Available values: today, 24_hours, yesterday, last_week, 7_days, this_week, this_month, 30_days, last_month, three_months, custom</param>
    /// <param name="start">The start date in YYYY-MM-DD format if custom is used</param>
    /// <param name="end">The end date in YYYY-MM-DD format if custom is used</param>
    public virtual Task<ReadingResponse> GetReadingsAsync(string sensor_id, int? offset, string setting, string start, string end)
    {
        return GetReadingsAsync(sensor_id, offset, setting, start, end, CancellationToken.None);
    }

    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <summary>
    /// Get sensor readings
    /// </summary>
    /// <returns>Successful response</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    /// /// <param name="setting">The time period. Available values: today, 24_hours, yesterday, last_week, 7_days, this_week, this_month, 30_days, last_month, three_months, custom</param>
    /// <param name="start">The start date in YYYY-MM-DD format if custom is used</param>
    /// <param name="end">The end date in YYYY-MM-DD format if custom is used</param>
    public virtual async Task<ReadingResponse> GetReadingsAsync(string sensor_id, int? offset, string setting, string? start, string? end, CancellationToken cancellationToken)
    {
        if (sensor_id == null)
            throw new ArgumentNullException("sensor_id");

        var urlBuilder_ = new System.Text.StringBuilder();
        urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/sensor/{sensor_id}/readings?");
        urlBuilder_.Replace("{sensor_id}", Uri.EscapeDataString(ConvertToString(sensor_id, System.Globalization.CultureInfo.InvariantCulture)));
        if (offset != null)
        {
            urlBuilder_.Append(Uri.EscapeDataString("offset") + "=").Append(Uri.EscapeDataString(ConvertToString(offset, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
        }
        if (setting != null)
        {
            urlBuilder_.Append(Uri.EscapeDataString("setting") + "=").Append(Uri.EscapeDataString(ConvertToString(setting, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
        }
        if (start != null)
        {
            urlBuilder_.Append(Uri.EscapeDataString("start") + "=").Append(Uri.EscapeDataString(ConvertToString(start, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
        }
        if (end != null)
        {
            urlBuilder_.Append(Uri.EscapeDataString("end") + "=").Append(Uri.EscapeDataString(ConvertToString(end, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
        }
        urlBuilder_.Length--;

        var client_ = _httpClient;
        var disposeClient_ = false;
        try
        {
            using (var request_ = new HttpRequestMessage())
            {
                request_.Method = new HttpMethod("GET");
                request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                PrepareRequest(client_, request_, urlBuilder_);

                var url_ = urlBuilder_.ToString();
                request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);

                PrepareRequest(client_, request_, url_);

                var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                var disposeResponse_ = true;
                try
                {
                    var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                    if (response_.Content != null && response_.Content.Headers != null)
                    {
                        foreach (var item_ in response_.Content.Headers)
                            headers_[item_.Key] = item_.Value;
                    }

                    ProcessResponse(client_, response_);

                    var status_ = (int)response_.StatusCode;
                    if (status_ == 200)
                    {
                        var objectResponse_ = await ReadObjectResponseAsync<ReadingResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                        if (objectResponse_.Object == null)
                        {
                            throw new ApiException("ReadingResponse was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                        }
                        return objectResponse_.Object;
                    }
                    else
                    {
                        var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                    }
                }
                finally
                {
                    if (disposeResponse_)
                        response_.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient_)
                client_.Dispose();
        }
    }

    public virtual Task<SensorsResponse> GetSensorsAsync()
    {
        return GetSensorsAsync(CancellationToken.None);
    }

    public virtual async Task<SensorsResponse> GetSensorsAsync(CancellationToken cancellationToken)
    {
        var urlBuilder_ = new System.Text.StringBuilder();
        urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/sensors/all");

        var client_ = _httpClient;
        var disposeClient_ = false;
        try
        {
            using (var request_ = new HttpRequestMessage())
            {
                request_.Method = new HttpMethod("GET");
                request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                PrepareRequest(client_, request_, urlBuilder_);

                var url_ = urlBuilder_.ToString();
                request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);

                PrepareRequest(client_, request_, url_);

                var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                var disposeResponse_ = true;
                try
                {
                    var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                    if (response_.Content != null && response_.Content.Headers != null)
                    {
                        foreach (var item_ in response_.Content.Headers)
                            headers_[item_.Key] = item_.Value;
                    }

                    ProcessResponse(client_, response_);

                    var status_ = (int)response_.StatusCode;
                    if (status_ == 200)
                    {
                        var objectResponse_ = await ReadObjectResponseAsync<SensorsResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                        if (objectResponse_.Object == null)
                        {
                            throw new ApiException("ReadingResponse was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                        }
                        return objectResponse_.Object;
                    }
                    else
                    {
                        var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                    }
                }
                finally
                {
                    if (disposeResponse_)
                        response_.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient_)
                client_.Dispose();
        }
    }

    /// <summary>
    /// Get sensor details
    /// </summary>
    /// <returns>Successful response</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public virtual Task<SensorResponse> GetSensorAsync(string sensor_id)
    {
        return GetSensorAsync(sensor_id, CancellationToken.None);
    }

    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <summary>
    /// Get sensor details
    /// </summary>
    /// <returns>Successful response</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public virtual async Task<SensorResponse> GetSensorAsync(string sensor_id, CancellationToken cancellationToken)
    {
        if (sensor_id == null)
            throw new ArgumentNullException("sensor_id");

        var urlBuilder_ = new System.Text.StringBuilder();
        urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/sensor/{sensor_id}");
        urlBuilder_.Replace("{sensor_id}", Uri.EscapeDataString(ConvertToString(sensor_id, System.Globalization.CultureInfo.InvariantCulture)));

        var client_ = _httpClient;
        var disposeClient_ = false;
        try
        {
            using (var request_ = new HttpRequestMessage())
            {
                request_.Method = new HttpMethod("GET");
                request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                PrepareRequest(client_, request_, urlBuilder_);

                var url_ = urlBuilder_.ToString();
                request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);

                PrepareRequest(client_, request_, url_);

                var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                var disposeResponse_ = true;
                try
                {
                    var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                    if (response_.Content != null && response_.Content.Headers != null)
                    {
                        foreach (var item_ in response_.Content.Headers)
                            headers_[item_.Key] = item_.Value;
                    }

                    ProcessResponse(client_, response_);

                    var status_ = (int)response_.StatusCode;
                    if (status_ == 200)
                    {
                        var objectResponse_ = await ReadObjectResponseAsync<SensorResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                        if (objectResponse_.Object == null)
                        {
                            throw new ApiException("ReadingResponse was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                        }

                        return objectResponse_.Object;
                    }
                    else
                    {
                        var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                    }
                }
                finally
                {
                    if (disposeResponse_)
                        response_.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient_)
                client_.Dispose();
        }
    }

    protected struct ObjectResponseResult<T>
    {
        public ObjectResponseResult(T responseObject, string responseText)
        {
            this.Object = responseObject;
            this.Text = responseText;
        }

        public T Object { get; }

        public string Text { get; }
    }

    public bool ReadResponseAsString { get; set; }

    protected virtual async Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(HttpResponseMessage response, IReadOnlyDictionary<string, IEnumerable<string>> headers, CancellationToken cancellationToken)
    {
        if (response == null || response.Content == null)
        {
            return new ObjectResponseResult<T>(default(T), string.Empty);
        }

        if (ReadResponseAsString)
        {
            var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            try
            {
                var typedBody = JsonSerializer.Deserialize<T>(responseText, JsonSerializerOptions);
                return new ObjectResponseResult<T>(typedBody, responseText);
            }
            catch (JsonException exception)
            {
                var message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
                throw new ApiException(message, (int)response.StatusCode, responseText, headers, exception);
            }
        }
        else
        {
            try
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                using (var gzStreamReader = new GZipStream(responseStream, CompressionMode.Decompress))
                using (var streamReader = new StreamReader(gzStreamReader))
                {
                    var jsonContent = await streamReader.ReadToEndAsync();
                    var jsonData = Encoding.UTF8.GetBytes(jsonContent);
                    var typedBody = JsonSerializer.Deserialize<T>(jsonData, JsonSerializerOptions);
                    return new ObjectResponseResult<T>(typedBody, string.Empty);
                }
            }
            catch (JsonException exception)
            {
                var message = "Could not deserialize the response body stream as " + typeof(T).FullName + ".";
                throw new ApiException(message, (int)response.StatusCode, string.Empty, headers, exception);
            }
        }
    }

    private string ConvertToString(object value, System.Globalization.CultureInfo cultureInfo)
    {
        if (value == null)
        {
            return "";
        }

        if (value is Enum)
        {
            var name = Enum.GetName(value.GetType(), value);
            if (name != null)
            {
                var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
                if (field != null)
                {
                    var attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(field, typeof(System.Runtime.Serialization.EnumMemberAttribute)) 
                        as System.Runtime.Serialization.EnumMemberAttribute;
                    if (attribute != null)
                    {
                        return attribute.Value != null ? attribute.Value : name;
                    }
                }

                var converted = Convert.ToString(Convert.ChangeType(value, Enum.GetUnderlyingType(value.GetType()), cultureInfo));
                return converted == null ? string.Empty : converted;
            }
        }
        else if (value is bool) 
        {
            return Convert.ToString((bool)value, cultureInfo).ToLowerInvariant();
        }
        else if (value is byte[])
        {
            return Convert.ToBase64String((byte[]) value);
        }
        else if (value.GetType().IsArray)
        {
            var array = Enumerable.OfType<object>((Array) value);
            return string.Join(",", Enumerable.Select(array, o => ConvertToString(o, cultureInfo)));
        }

        var result = Convert.ToString(value, cultureInfo);
        return result == null ? "" : result;
    }
}