# Pandora FixOpenaiApi


## 让部分不方便修改 OpenAI API 地址的程序，可以继续使用 fakeopen 的 OpenAI API
## 使用方法
* 下载程序 <a href="https://github.com/pengzhile/pandora/tree/master/src/pandora/FixOpenaiApi/FixOpenaiApi.zip" target="_blank" title="下载地址">https://github.com/pengzhile/pandora/tree/master/src/pandora/FixOpenaiApi/FixOpenaiApi.zip</a>
* 以管理员身份运行 `FixOpenaiApi.exe` ,第一次运行会提示安装证书,安装证书后,重新打开需使用API的软件即可。

## 自行编译
* 安装 SDK <a href="https://dotnet.microsoft.com/zh-cn/download/dotnet/7.0" target="_blank" title="SDK下载地址">https://dotnet.microsoft.com/zh-cn/download/dotnet/7.0</a>
* 编译 dotnet publish -r win-x64
* 具体参考 <a href="https://learn.microsoft.com/zh-cn/dotnet/core/deploying/" target="_blank" title="SDK下载地址">https://learn.microsoft.com/zh-cn/dotnet/core/deploying/</a>