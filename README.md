# GeforceExperienceHotKey

[![Build and Release](https://github.com/denpadokei/GeforceExperienceHotKey/actions/workflows/build-and-release.yml/badge.svg)](https://github.com/denpadokei/GeforceExperienceHotKey/actions/workflows/build-and-release.yml)

リザルト画面に REC ボタンを追加し、押すと GeForce Experience の保存ホットキー (Alt+F10) を送信する MOD です。シーン切り替え時に Beat Saber のウィンドウをアクティブにします。

## 機能

- リザルト画面 (`ResultsViewController`) に BSML で REC ボタンを追加する
- REC ボタンを押すと `keybd_event` で Alt+F10 を送信する
- 押した後はボタンを無効化し、リザルト画面を開き直すと再び有効になる
- シーンが切り替わるたびに `Beat Saber` ウィンドウを検索してフォアグラウンドへ移動する
- 最小化されている場合は復元し、フォアグラウンドロックを一時的に解除して前面化する
- ゲーム内の設定画面や設定ファイルは持たない

## 動作環境

| 項目 | 内容 |
| --- | --- |
| Beat Saber | 1.12.2 |
| 依存 MOD | BSIPA 4.1.3 以降 / BS Utils / BeatSaberMarkupLanguage (コードが参照) |

## インストール

1. Beat Saber を 1.12.2 に合わせる (BSManager などのバージョン管理ツールを推奨)
2. BSIPA と上記の依存 MOD をインストールする
3. [Releases](https://github.com/denpadokei/GeforceExperienceHotKey/releases) から `GeforceExperienceHotKey-1.0.0-bs1.12.2.zip` をダウンロードし、`Plugins` フォルダをゲームフォルダへ上書きコピーする

## 使い方

リザルト画面に表示される REC ボタンをコントローラでポイントして押します。押すと GeForce Experience のホットキー `Alt+F10` が送信され、録画の保存などが実行されます。

GeForce Experience 側のキーコンフィグが変わっていると動作しません。使用前に GeForce Experience のホットキー設定を確認してください。

## ビルド

CodeTaskFactory を使う従来形式の targets (`BuildTargets.targets`) を含むため、`dotnet build` ではなく Visual Studio または Developer PowerShell の `msbuild` でビルドします。

```powershell
git clone https://github.com/denpadokei/GeforceExperienceHotKey.git
cd GeforceExperienceHotKey
msbuild .\GeforceExperienceHotKey\GeforceExperienceHotKey.csproj /t:Build /p:Configuration=Release
```

`BeatSaberDir` に Beat Saber のインストール先を `/p:BeatSaberDir="C:\Program Files\Beat Saber"` のように指定すると、ゲームの参照アセンブリを解決できます。

GitHub Actions の `build-and-release.yml` が push ごとにビルドし、`X.X.X` 形式のタグを push するとリリースを作成します。

## ライセンス

MIT License (`LICENSE.md`)
