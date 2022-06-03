import { useEffect, useState } from "react";
import request, { API } from "../Api";
import { useHistory } from "react-router-dom";
import LoadingPage from "../pages/LoadingPage";

const CheckLoinRoute = ({ setChats, children, setMyData }) => {
  const [isLoading, setIsLoading] = useState(false);
  const [isLogin, setIsLogin] = useState(false);

  const history = useHistory();

  useEffect(() => {
    const username = localStorage.getItem("user");

    if (!username) {
      setIsLogin(false);

      return setIsLoading(true);
    }

    const data = new FormData();
    data.append("username", username);

    request({
      url: "/info",
      method: "POST",
      data,
    })
      .then(({ data: { code, data } }) => {
        if (code === 200) {
          const { displayname, avatar, contacts } = data;

          setChats(contacts);
          setMyData({ img: `${API}/${avatar}`, user: displayname });
          setIsLogin(true);
        } else {
          setIsLoading(true);
        }

        setIsLoading(true);
      }) //
      .catch(() => {
        setIsLoading(true);
        setIsLoading(true);
      });
  }, [setMyData, setChats]);

  return isLoading && isLogin ? (
    history.push("/f")
  ) : isLoading && !isLogin ? (
    children
  ) : (
    <LoadingPage />
  );
};

export default CheckLoinRoute;
