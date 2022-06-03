const LoadingPage = () => {
  return (
    <main className="loading-container d-flex justify-content-center align-items-center text-light flex-column gap-2">
      <div className="spinner-border text-secondary" role="status">
        <span className="sr-only"></span>
      </div>
      <span>Loading...</span>
    </main>
  );
};

export default LoadingPage;
