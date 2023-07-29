using BlazorChatSignalR.Server.Hubs;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
//This code adds the MIME type "application/octet-stream" to the list of MIME types supported by the response compression middleware. This MIME type is used for binary
// data, such as files, and allows the response compression middleware to compress binary data before sending it to the client.
builder.Services.AddResponseCompression(options =>
    options.MimeTypes = ResponseCompressionDefaults
    .MimeTypes
    .Concat(new[] { "application/octet-stream" })
    );
var app = builder.Build();
//This line of code is used to enable response compression in an ASP.NET Core application. Response compression is a technique used to reduce the size of the response
// sent from the server to the client. This can help reduce the amount of data sent over the network, which can improve the performance of the application.
app.UseResponseCompression();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
//This line of code is used to map a ChatHub class to a specific URL path. This allows the ChatHub class to be accessed from the URL path "/chathub". This is typically
// used in ASP.NET Core applications to create a real-time communication channel between the server and the client.
app.MapHub<ChatHub>("/chathub");
app.MapFallbackToFile("index.html");

app.Run();
