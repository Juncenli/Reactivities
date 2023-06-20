import ReactDOM from 'react-dom/client';
import './app/layout/styles.css';
import 'semantic-ui-css/semantic.min.css'
import App from './app/layout/App';
import reportWebVitals from './reportWebVitals';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
  // 这个会导致call 两次Axios 因为渲染了两次
  // <React.StrictMode>
    <App />
  // </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
