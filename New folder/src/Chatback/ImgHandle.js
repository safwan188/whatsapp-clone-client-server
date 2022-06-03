function HandleImg({ image }) {
  return (
    <p id="mess">
      <img id="display-image" width={300} height={200} src={image}></img>
    </p>
  );
}
export default HandleImg;
