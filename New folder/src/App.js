import { useCallback, useEffect } from "react";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";

import ProductRoute from "./ProductRoute";
import CheckLoinRoute from "./CheckLoinRoute";

import { API } from "./Api";
import {
  HttpTransportType,
  HubConnectionBuilder,
  LogLevel,
} from "@microsoft/signalr";

import Left from "./Leftside/Leftside";
import Chat from "./Chatback/Chatside";
import React, { useState } from "react";
import Register from "./login/register";
import Login from "./login/Log";
import LoadingPage from "./pages/LoadingPage";

import img2 from "./j.jpeg";

import defaultImg from "./login/zoro.jpeg";

import "./temp.css";
import "./App.css";

function App() {
  //these state is the user state and we update them on every operation wich render the component app to keep it update
  const [chatstate, setchatstate] = useState({ name: "" }); //name
  const [chatstateImg, setchatstateImg] = useState({ img: {} }); //image
  const [chatstateId, setchatstateId] = useState({ id: "1" }); //id
  const [x, setx] = useState(1); //id
  const [userpass, setuserpass] = useState(""); //name

  const [connection, setConnection] = useState();
  const [isConnectLoad, setIsConnectLoad] = useState(false);

  const [chats, setChats] = useState([]);
  const [myData, setMyData] = useState({});

  console.log(connection)

  const initConnection = useCallback(async () => {
    const username = localStorage.getItem("user");

    if (!username) return setIsConnectLoad(true);

    try {
      const connection = new HubConnectionBuilder()
        .withUrl(`${API}/chat`, {
          skipNegotiation: true,
          transport: HttpTransportType.WebSockets,
        })
        .configureLogging(LogLevel.Information)
        .build();

      connection.on("ReceiveMessage", (messagesData) => {
        const { from, to, content } = messagesData;

        const isToMe = to.username === username;

        if (!isToMe) return;

        setChats((prev) => {
          const cuurentChat = {
            id: Math.floor(Math.random() * 500000),
            username: from.username,
            displayName: from.displayName,
            array: [
              {
                id: Math.floor(Math.random() * 500000),
                str: content,
                type: "m",
                fromMe: false,
              },
            ],
          };

          const existChat = prev.find(chat => chat.username === from.username);


          if(existChat) {
            existChat.array.push(cuurentChat.array[0])
            
            return [...prev]
          }

          // new Chat
          return [...prev, cuurentChat];
        });
      });

      connection.on("AddContact", (messagesData) => {
        const { from, to } = messagesData;

        const isToMe = to.username === username;

        if (!isToMe) return;

        setChats((prev) => {
          const cuurentChat = {
            id: Math.floor(Math.random() * 500000),
            username: from.username,
            displayName: from.displayName,
            array: [],
          };

          // new Chat
          return [...prev, cuurentChat];
        });
      });

      connection.onclose(() => setConnection());

      await connection.start();
      await connection.invoke("JoinRoom", username);

      setConnection(connection);
      setIsConnectLoad(true);
    } catch (e) {
      alert(e);
      setIsConnectLoad(true);
    }
  }, []);

  const sendMessage = async (messageData) => {
    const username = localStorage.getItem("user");

    const { from, to, content } = messageData;

    try {
      await connection.invoke("SendMessage", from, to, content, username);
    } catch (e) {
      console.log(e);
    }
  };

  useEffect(() => {
    initConnection();
  }, [initConnection]);

  function upatechat({ name, img, s, id }) {
    setchatstate((chatstate.name = { name }));
    setchatstateId((chatstateId.id = { id }));
    setchatstateImg((chatstateImg.img = { img: img || defaultImg }));
    setx(2);
  }

  return (
    <>
      {!isConnectLoad ? (
        <LoadingPage />
      ) : x === 1 ? (
        <Router>
          <div className="row container-fluid ttt">
            <div id="ff" className="row">
              <Switch>
                <Route exact path="/">
                  <CheckLoinRoute setChats={setChats} setMyData={setMyData}>
                    <Login setChats={setChats} setMyData={setMyData} />
                  </CheckLoinRoute>
                </Route>
                <Route exact path="/a">
                  <Register />
                </Route>
                <Route exact path="/f">
                  <ProductRoute setChats={setChats} setMyData={setMyData}>
                    <Left
                      setChats={setChats}
                      myData={myData}
                      infos={chats}
                      upatechat={upatechat}
                      y={x}
                      setx={setx}
                      userpass={userpass}
                    />
                    <div className="col-9">
                      <img id="ci" src={img2} alt=""></img>
                    </div>
                  </ProductRoute>
                </Route>
              </Switch>
            </div>
          </div>
        </Router>
      ) : (
        <div className="row container-fluid ttt">
          <div id="ff" className="row">
            <Left
              setChats={setChats}
              myData={myData}
              infos={chats}
              upatechat={upatechat}
              y={x}
              setx={setx}
              userpass={userpass}
            />
            <Chat
              infos={chats}
              sendMessage={sendMessage}
              chatstate={chatstate}
              chatstateId={chatstateId}
              chatstateImg={chatstateImg}
              upatechat={upatechat}
              setChats={setChats}
            />
          </div>
        </div>
      )}
    </>
  );
}

export default App;
