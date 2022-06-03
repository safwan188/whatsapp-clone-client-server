import Handle from "./Chatjandle";
import Typo from "./Type";
import HandleImg from "./ImgHandle";
import Vhandle from "./VideoHandle";
import Audiohandle from "./AudioHandle";
import { useEffect, useState } from "react";

function Chat({
  chatstate,
  chatstateId,
  chatstateImg,
  upatechat,
  sendMessage,
  infos,
  setChats,
}) {
  const [currentChat, setCurrentChat] = useState([]);

  useEffect(() => {
    const currentChat = infos.find((chat) => chat.id === chatstateId.id);

    setCurrentChat({ ...currentChat });
  }, [chatstateId, infos]);

  function updatechat({ name, img, s, typ, id }) {
    upatechat({ name, img, s, id });
  }

  return (
    <div className="col-9">
      <div id="userrow" className="row">
        <ul className="list-group">
          <li className="list-group-item d-flex  align-items-center">
            <img className="rounded-circle" src={chatstateImg.img} alt="" />
            <span style={{ color: "white" }} className="w-100 ms-3">
              {chatstate.name}
            </span>
          </li>
        </ul>
      </div>
      <div></div>
      <div id="backchat" className="row">
        <div id="chathere">
          {currentChat.array?.length &&
            currentChat.array.map((s, key) =>
              s.type === "m" || s.typ === "m" ? (
                <Handle fromMe={s.fromMe} s={s.str} key={key} />
              ) : null
            )}
          {/* {currentChat.array?.length &&
            currentChat.array.map((s, key) =>
              s.type === "m" ? (
                <Handle fromMe={s.fromMe} s={s.str} key={key} />
              ) : s.type === "img" ? (
                <HandleImg image={s.str} />
              ) : s.type === "v" ? (
                <Vhandle video={s.str} />
              ) : s.type === "A" ? (
                <Audiohandle audio={s.str} />
              ) : null
            )} */}
        </div>
      </div>
      <Typo
        setChats={setChats}
        infos={infos}
        sendMessage={sendMessage}
        name={chatstate.name}
        img={chatstateImg.img}
        id={chatstateId.id}
        updatechat={updatechat}
      />
    </div>
  );
}
export default Chat;
