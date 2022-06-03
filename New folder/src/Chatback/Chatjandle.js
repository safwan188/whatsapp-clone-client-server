function Handle({ s, fromMe }) {
  return (
    <div>
      <p
        className={`chat-message${!fromMe ? " from-user" : ""}`}
        style={{ color: "white" }}
        id="fm"
      >
        {" "}
        {s}{" "}
      </p>
    </div>
  );
}
export default Handle;
