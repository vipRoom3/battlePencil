# battlePencil

## 環境
- Unity:2022.3.10f1
- WebGL
- .NET

### Lint
ref: https://www.nuget.org/packages/dotnet-format/
#### Install
Powershell で実行してます。
```powershell
$ dotnet new tool-manifest
$ dotnet tool install --local dotnet-format
```

`.config/dotnet-tools.json` が生成されます。

#### Check
```powershell
$ dotnet format --verify-no-changes battlePencil.csproj --include Assets\Scripts\
```
何も出力されなければOK。

:warning: 以下のような出力がされたら Format をしよう
```powershell
***:***\battlePencil\Assets\Scripts\Battle\BattleService.cs(1,1): error IMPORTS: インポートの順序を修正します。 [***:***\battlePencil\battlePencil.csproj]
```

#### Format
Check が失敗したときや、GitHub Workflows の `lint check` が落ちていたときに実行しよう
```powershell
$ dotnet format style battlePencil.csproj --include Assets\Scripts\
# 拡張機能で format してくれてるはずなので基本的には実行しなくていい
$ dotnet format whitespace battlePencil.csproj --include Assets\Scripts\
```


