


import Guest from "./Guestside";
import { useEffect, useRef, useState } from "react";

import request from "../Api";

function Left({ upatechat, y, setx, userpass, infos, myData, setChats }) {
  const [data, setData] = useState({});

  useEffect(() => {
    setData(myData);
  }, [myData]);

  const MessageList = infos.map((message, key) => {
    return (
      <Guest
        chexk={chexk}
        name={message.displayName}
        message={message?.array?.[0]?.str}
        time="05:10"
        img={message.avatar}
        id={message.id}
        key={key}
      />
    );
  });

  function chexk({ name, img, s, id }) {
    upatechat({ name, img, s, id });
  }

  const sendbox = useRef();

  const addContact = (e) => {
    e.preventDefault();

    const username = sendbox.current.value;
    sendbox.current.value = "";

    const data = new FormData();
    data.append("uname", localStorage.getItem("user"));
    data.append("fusername", username);

    request({
      method: "POST",
      url: "/newcontact",
      data,
    }) //
      .then(({ data: { code, data, error } }) => {
        if (code === 200) {
          const { displayName, avatar, time } = data;

          const newChat = {
            id: Math.floor(Math.random() * 500000),
            name: displayName,
            username: username,
            img: avatar,
            time,
            array: [],
          };

          setChats((prev) => [...prev, newChat]);

          alert(`"${displayName}" Successfully added to your contacts`);

          return;
        }

        alert(error);
      }) //
      .catch((err) => {
        alert(err);
      });
  };

  return (
    <div id="try" className="col-3 bg-darkgray ">
      <div id="userrow" className="row ">
        <div className="d-flex mb-3 mt-3 align-items-center">
          <img
            id="userimg"
            className="padding-left rounded-circle"
            src={data?.img}
            alt=""
          ></img>

          <span id="user" style={{ color: "white" }} className="ms-3 d-flex">
            {data?.user}{" "}
          </span>
          <button
            type="button "
            className="btn"
            data-bs-toggle="modal"
            data-bs-target="#staticBackdrop2"
          >
            <i
              style={{ color: "white" }}
              className="bi bi-person-plus-fill"
            ></i>
          </button>
          <div
            className="modal fade"
            id="staticBackdrop2"
            data-bs-backdrop="static"
            data-bs-keyboard="false"
            tabIndex="-1"
            aria-labelledby="staticBackdropLabel"
            aria-hidden="true"
          >
            <div className="modal-dialog">
              <div className="modal-content">
                <div className="modal-header">
                  <h5 className="modal-title" id="staticBackdropLabel2">
                    Add guest
                  </h5>
                  <button
                    type="button"
                    className="btn-close"
                    data-bs-dismiss="modal"
                    aria-label="Close"
                  ></button>
                </div>
                <form onSubmit={addContact}>
                  <div className="modal-body">
                    <div className="mb-3">
                      <label htmlFor="formFile" className="form-label"></label>
                      <input
                        ref={sendbox}
                        className="form-control"
                        placeholder="type user Name"
                        type="text"
                        id="input_img"
                      />
                    </div>
                  </div>
                  <div className="modal-footer">
                    <button
                      type="submit"
                      className="btn btn-primary"
                      data-bs-dismiss="modal"
                    >
                      Add
                    </button>
                  </div>
                </form>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div
        id="contact_div"
        className="table-wrapper-scroll-y my-custom-scrollbar"
      >
        {MessageList}
        <table className="table table-bordered table-striped mb-0"></table>
      </div>
    </div>
  );
}
export default Left;
