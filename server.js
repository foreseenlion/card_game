// These two first lines create your server instance
var server = require('http').createServer();
var io = require('socket.io')(server);

var watiForGame = null;
var games = {};
var playersGames = {}

// This defines the port that we'll be listening to
server.listen(3000);

io.sockets.on('connection', function (socket) {

	console.log('User connected: ' + socket.id);
	// Mechanizm tworzenia i przydzielania gier
	checkGame(socket.id)

	socket.emit('connectionEstabilished', { id: socket.id });
	socket.broadcast.emit('connectionEstabilished', { id: socket.id });

	socket.on('move', function (data) {
		move(data, socket)
	});

	socket.on('end', function (data) {
		tureEnd(data, socket)
	});

	socket.on('playerMove', function (data) {
		playerMove(data, socket)
	});

});

function tureEnd(data, socket) {
	console.log("hi")
	var gameTmp = games[playersGames[socket.id]]
	var idPlayer = replyPlayer(gameTmp, socket.id)
	socket.emit("tureEnd", { id: socket.id, ture: "0" });
	socket.to(socket.id).emit("tureEnd", { id: socket.id, ture: "0" });
}

function move(data, socket) {
	console.log(data.PoleY)
	console.log(data.PoleX)
	var gameTmp = games[playersGames[socket.id]]
	var idPlayer = replyPlayer(gameTmp, socket.id)
	// idPlayer powinno byc argumentem funkcji to  (nie jest bo debaguje)
	socket.emit("foreignMessage", { id: socket.id, x: data.PoleX, y: data.PoleY });
	socket.to(socket.id).emit("foreignMessage", { id: socket.id, y: data.PoleY ,x: data.PoleX,});
}

function odbicieLustrzane(pozycjaGracza){
return 7-  pozycjaGracza
}



function playerMove(data, socket) {
	var gameTmp = games[playersGames[socket.id]]
	var idPlayer = replyPlayer(gameTmp, socket.id)
	// idPlayer powinno byc argumentem funkcji to 
	socket.emit("moveTo", {
		id: socket.id,
		idPionka: data.idPionka,
		poleStartoweX: odbicieLustrzane(data.poleStartoweX),
		poleStartoweY: odbicieLustrzane(data.poleStartoweY),
		poleDoceloweX: odbicieLustrzane(data.poleDoceloweX),
		poleDoceloweY: odbicieLustrzane(data.poleDoceloweY),
	});
	socket.to(socket.id).emit("moveTo", {
		id: socket.id,
		idPionka: data.idPionka,
		poleStartoweX: odbicieLustrzane(data.poleStartoweX),
		poleStartoweY: odbicieLustrzane(data.poleStartoweY),
		poleDoceloweX: odbicieLustrzane(data.poleDoceloweX),
		poleDoceloweY: odbicieLustrzane(data.poleDoceloweY),
	});
}



function replyPlayer(gameTmp, idPlayer) {
	if (gameTmp.userWhite != idPlayer) {
		return gameTmp.userWhite;
	} else {
		return gameTmp.userBlack;
	}
}


function checkGame(idPlayer) {
	// Zmiena waitForGame zawiera id gry do ktorej moze dolaczyc jeszcze jeden gracz
	if (watiForGame == null) {
		// jezeli zmiena jest pusta tworzona jest nowa gra
		var id = idGenerate()
		games[id] = new game()
		games[id].gameId = id;
		games[id].userWhite = idPlayer
		watiForGame = id
		playersGames[idPlayer] = id
	}
	else {
		games[watiForGame].userBlack = idPlayer
		playersGames[idPlayer] = watiForGame
		watiForGame = null
	}
}



function idGenerate() {
	return '_' + Math.random().toString(36).substr(2, 9);
};


class game {
	gameId;
	userWhite;
	userBlack;
}