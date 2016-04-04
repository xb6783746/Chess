%systemroot%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe Chess.sln
If Not Exist "Game" (
	md Game
)
If Not Exist "Game\Screens" ( 
	md Game\Screens 
)

copy ChessClient\bin\Debug\ChessClient.exe Game
copy ChessServer\bin\Debug\ChessServer.exe Game
copy ClientAPI\bin\Debug\ClientAPI.dll Game
copy Network\bin\Debug\Network.dll Game
copy GameTemplate\bin\Debug\GameTemplate.dll Game

copy GameScreen\bin\Debug\GameScreen.dll Game\Screens
copy LogInScreen\bin\Debug\LogInScreen.dll Game\Screens
copy MainScreen\bin\Debug\MainScreen.dll Game\Screens
copy WaitScreen\bin\Debug\WaitScreen.dll Game\Screens
copy Rendering\bin\Debug\Rendering.dll Game\Screens

echo 127.0.0.1> Game\config.txt
echo 8888>> Game\config.txt
