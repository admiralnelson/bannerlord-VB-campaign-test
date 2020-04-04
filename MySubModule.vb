Imports TaleWorlds.CampaignSystem
Imports TaleWorlds.Core
Imports TaleWorlds.MountAndBlade
Imports System.Diagnostics
Imports TaleWorlds.CampaignSystem.SandBox.GameComponents

Namespace Global.MyVBProject
    Public Class MySubModule
        Inherits MBSubModuleBase

        Protected Overrides Sub OnSubModuleLoad()
            MyBase.OnSubModuleLoad()
        End Sub

        Protected Overrides Sub OnGameStart(game As Game, gameStarterObject As IGameStarter)
            Dim campaign = game.GameType
            If (campaign Is Nothing) Then
                Debug.WriteLine("oops!")
                Exit Sub
            End If
            Dim campaignStarter = CType(gameStarterObject, CampaignGameStarter)
            AddBehaviour(campaignStarter)
        End Sub

        Protected Overrides Sub OnBeforeInitialModuleScreenSetAsRoot()
            MyBase.OnBeforeInitialModuleScreenSetAsRoot()
            Dim ver = System.Environment.Version
            InformationManager.ShowInquiry(New InquiryData(
                "Net Enviroment",
                $"running on version {ver}",
                True,
                False,
                "Accept",
                "",
                Sub()
                    'Environment.Exit(1)
                End Sub,
                Sub()

                End Sub))
        End Sub

        Private Sub AddBehaviour(gameInit As CampaignGameStarter)
            gameInit.AddBehavior(New SimpleDayCounter)
        End Sub

        Friend Class SimpleDayCounter
            Inherits CampaignBehaviorBase
            Private dateTime As DateTime
            Public Sub New()
                dateTime = New DateTime(1085, 1, 1)
            End Sub

            Public Overrides Sub RegisterEvents()
                CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(Me,
                   New Action(Of CampaignGameStarter)(AddressOf OnSessionLaunched))
                CampaignEvents.DailyTickEvent.AddNonSerializedListener(Me,
                    New Action(AddressOf DailyTick))
            End Sub

            Public Sub DailyTick()
                dateTime.AddDays(1)
                InformationManager.DisplayMessage(
                            New InformationMessage($"date is now {dateTime.ToString("D")}"))
            End Sub

            Public Sub OnSessionLaunched()

            End Sub

            Public Overrides Sub SyncData(dataStore As IDataStore)

            End Sub
        End Class

    End Class

End Namespace