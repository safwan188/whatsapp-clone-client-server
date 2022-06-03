import { useRef, useState } from "react";
import { useHistory } from "react-router-dom";

import request, { API } from "../Api";

function Login({ setMyData, setChats }) {
  const sendbox = useRef();
  const sendbox1 = useRef();
  let history = useHistory();

  const [requestLoading, setRequestLoading] = useState(false);

  const check = () => {
    const [username, password] = [
      sendbox.current.value,
      sendbox1.current.value,
    ];

    setRequestLoading(true);

    request({
      method: "GET",
      url: `/login?username=${username}&password=${password}`,
    })
      .then(({ data: { code, error, data } }) => {
        if (code === 200) {
          localStorage.setItem("user", username);
          const { avatar, displayname, contacts } = data;
          setChats(contacts);
          setMyData({ img: `${API}/${avatar}`, user: displayname });

          return history.push("/f");
        }

        alert(error);

        setRequestLoading(false);
      })
      .catch((err) => {
        alert(err.message);
        setRequestLoading(false);
      });
  };

  const register = () => {
    history.push("/a");
  };

  return (
    <div className="form-body">
      <div className="row">
        <div className="form-holder">
          <div className="form-content">
            <div className="form-items">
              <h3>Log In</h3>
              <p>Fill in the data below.</p>
              <form
                onSubmit={(e) => {
                  e.preventDefault();
                }}
                className="requires-validation"
                noValidate
              >
                <div className="col-md-12">
                  <input
                    ref={sendbox}
                    className="form-control"
                    type="text"
                    name="name"
                    placeholder="username"
                    required
                  ></input>
                  <div className="valid-feedback">Username field is valid!</div>
                  <div className="invalid-feedback">
                    Username field cannot be blank!
                  </div>
                </div>

                <div className="col-md-12">
                  <input
                    ref={sendbox1}
                    className="form-control"
                    type="password"
                    name="password"
                    placeholder="Password"
                    required
                  ></input>
                  <div className="valid-feedback">Password field is valid!</div>
                  <div className="invalid-feedback">
                    Password field cannot be blank!
                  </div>
                </div>
                <div className="form-button mt-3">
                  <button
                    onClick={check}
                    id="submita"
                    type="submit"
                    className="btn btn-primary"
                    disabled={requestLoading}
                  >
                    log in
                  </button>
                </div>
                <div className="form-button mt-3">
                  <p>Not a member yet </p>
                  <button
                    onClick={register}
                    id="submit"
                    type="submit"
                    className="btn btn-primary"
                  >
                    join us now
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
export default Login;
