function Audiohandle({ audio }) {
  return (
    <p>
      <audio id="audios" controls>
        <source src={audio} type="audio/ogg"></source>
      </audio>
    </p>
  );
}
export default Audiohandle;
