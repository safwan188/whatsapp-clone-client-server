import { useRef } from "react";
import React from "react";

function Typo({ name, img, id, updatechat, sendMessage, infos, setChats }) {
  const mlist = () => {
    for (var i = 0; i < infos.length; i++) {
      if (id === infos[i].id) {
        return i;
      }
    }
  };

  const vidU = useRef();
  const m = mlist();
  const sendbox = useRef();

  const send = () => {
    if (!sendbox.current.value) return alert("type message");

    setChats((prev) => {
      const cureentChatIdx = prev.findIndex((chat) => chat.id === id);

      prev[cureentChatIdx].array.push({
        str: sendbox.current.value,
        type: "m",
        fromMe: true,
      });

      return [...prev];
    });

    sendbox.current.value = "";
  };

  const upload = useRef();

  const onClick = (e) => {
    upload.current.click();
  };

  const handleVideoUpload = () => {
    var base64data;
    var reader = new FileReader();
    reader.onload = function () {
      base64data = reader.result;
      //  var i =base64data.split(',')[1];
      var i = base64data;
      var typ = "v";
      const r = { str: i, typ: typ };
      infos[m].array.push(r);
      infos[m].message = "video";
      updatechat({ name, img, i, typ, id });
    };
    var video = document.getElementById("vidd").files[0];
    reader.readAsDataURL(video);
    vidU.current.value = null;
  };

  var x;

  const startaudio = () => {
    navigator.mediaDevices.getUserMedia({ audio: true }).then((stream) => {
      const mediaRecorder = new MediaRecorder(stream);
      mediaRecorder.start();

      const audioChunks = [];
      mediaRecorder.addEventListener("dataavailable", (event) => {
        audioChunks.push(event.data);
      });

      mediaRecorder.addEventListener("stop", () => {
        const audioBlob = new Blob(audioChunks);
        const i = URL.createObjectURL(audioBlob);
        var typ = "A";
        const r = { str: i, typ: typ };
        infos[m].array.push(r);
        infos[m].message = "audio";
        updatechat({ name, img, i, typ, id });
      });
      x = mediaRecorder;
    });
  };

  const fff = () => {
    stanl(x);
  };

  const stanl = (x) => {
    x.stop();
  };

  const handleImageUpload = () => {
    var base64data;
    var reader = new FileReader();
    reader.onload = function () {
      base64data = reader.result;
      //  var i =base64data.split(',')[1];
      var i = base64data;
      var typ = "img";
      const r = { str: i, typ: typ };
      infos[m].array.push(r);
      infos[m].message = "img";
      updatechat({ name, img, i, typ, id });
    };
    var image = document.getElementById("upload").files[0];
    reader.readAsDataURL(image);
    upload.current.value = null;
  };

  const vidclick = () => {
    vidU.current.click();
  };

  const something = (event) => {
    if (event.keyCode === 13) {
      onSubmit();
    }
  };

  const onSubmit = () => {
    const from = localStorage.getItem("user");
    const to = infos.find((chat) => chat.id === id)?.username;
    const content = sendbox.current.value;

    const message = { from, to, content };

    sendMessage(message);
    send();
  };

  return (
    <div id="chatrow" className="row">
      <div className="btn-group" role="group" aria-label="Basic example">
        <div className="btn-group dropup">
          <button
            type="button"
            className="btn btn-secondary dropdown-toggle"
            data-bs-toggle="dropdown"
            aria-expanded="false"
          ></button>
          <ul className="dropdown-menu">
            <input
              ref={upload}
              id="upload"
              type="file"
              onChange={handleImageUpload}
              accept="Image/*"
              hidden="hidden"
            ></input>
            <button onClick={onClick} type="button" className="btn">
              <i className="bi bi-images"></i>
            </button>
            <input
              ref={vidU}
              id="vidd"
              type="file"
              onChange={handleVideoUpload}
              accept="Video/*"
              hidden="hidden"
            ></input>
            <button onClick={vidclick} type="button" className="btn">
              <i className="bi bi-file-earmark-play-fill"></i>
            </button>
          </ul>
        </div>
        <button
          id="audiobtn"
          onClick={startaudio}
          type="button"
          className="btn"
        >
          start recording<i className="bi bi-mic-fill"></i>
        </button>
        <button id="audiobtn" onClick={fff} type="button" className="btn">
          stop recording<i className="bi bi-stop-circle"></i>
        </button>

        <input
          onKeyDown={something}
          ref={sendbox}
          id="x"
          type="text"
          className="form-control"
          placeholder="type here"
        ></input>
        <button onClick={onSubmit} type="button" className="btn btn-success">
          send
        </button>
      </div>
    </div>
  );
}
export default Typo;
