import { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import request, { API } from "../Api";
import LoadingPage from "../pages/LoadingPage";

const ProductRoute = ({ children, myData, setMyData, setChats }) => {
  const [isLoading, setIsLoading] = useState(false);
  const history = useHistory();

  useEffect(() => {
    if (myData?.user) setIsLoading(true);
  }, [myData]);

  useEffect(() => {
    const username = localStorage.getItem("user");

    if (!username) return history.push("/");

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
        } else {
          history.push("/");
        }

        setIsLoading(true);
      }) //
      .catch(() => {
        setIsLoading(true);
        history.push("/");
      });
  }, [history, setMyData, setChats]);

  if (!isLoading) return <LoadingPage />;

  return children;
};

export default ProductRoute;
