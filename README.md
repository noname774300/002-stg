# 002-stg

## TODO

- [x] シーンの切り替え調査 →SceneManager
- [x] セーブ調査 →PlayerPrefs
- [x] タイトルシーン作成
  - [x] 戦闘シーンへの移行
- [ ] 戦闘シーン作成
  - [x] Player、Enemy、Shot を文字と図形で表示
  - [x] Player 表示
  - [x] Player 移動（仮想 Pad）
  - [x] Enemy 配置
  - [x] Enemy 移動
  - [x] 視界回転
  - [x] マップ
    - [x] 調査 →Camera、Render Texture、Layer
    - [x] 表示
    - [x] Order in Layer
    - [x] Tilemap
    - [x] マップ処理（敵は Player より広い範囲を移動できる）
  - [ ] Player の攻撃
    - [ ] 自動射撃
    - [ ] 当たり判定
    - [ ] トリガーボタンで制御
    - [ ] 狙い切り換え
      - [ ] 近
      - [ ] 弱
      - [ ] 強
    - [ ] 武器切り換えボタンで切り換え
      - [ ] ロケットランチャー
      - [ ] ライフル
      - [ ] ショットガン
    - [ ] Player HP バー表示（分数も）
  - [ ] キーボード、ゲームパッドに対応
    - [x] InputSystem [Unity の新入力システム・Input System を使おう – Unity for Pro](https://forpro.unity3d.jp/unity_pro_tips/2021/05/20/1957/)
    - [x] Joystick Pack の DynamicJoystick 配置
    - [ ] 仮想ボタン配置
  - [ ] 戦闘終了演出
    - [ ] タイトルシーンへ移行
  - [ ] Enemy の攻撃
    - [ ] 自動射撃
    - [ ] 当たり判定
    - [ ] 体当たり
  - [ ] Enemy Hive
    - [ ] 自動ポップ
  - [ ] Enemy 破壊演出
    - [ ] アイテム生成（HP 回復、バリア、資源（金））
    - [ ] アイテム取得
- [ ] 複数ステージ
  - [ ] ステージデータ形式調査
  - [ ] ステージ生成
  - [ ] ステージ選択シーン追加
    - [ ] 戦闘シーンへ移行
    - [ ] 戦闘シーンクリア後ステージ選択シーンへ
  - [ ] 最終ステージ終了後エンディング
- [ ] セーブ
- [ ] 作りこみ
  - [ ] Player、Enemy、Shot 画像作成、差し替え
  - [ ] BGM 入手、設定
  - [ ] SE 入手、設定
