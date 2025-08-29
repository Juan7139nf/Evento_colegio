// src/firebaseConfig.js
// // Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import { getAnalytics } from "firebase/analytics";
import { getAuth, GoogleAuthProvider } from "firebase/auth";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional

const firebaseConfig = {
  apiKey: "AIzaSyDjBp0aZteqFjD18opAJcs0lUbJc3A6wAs",
  authDomain: "evento-colegio.firebaseapp.com",
  projectId: "evento-colegio",
  storageBucket: "evento-colegio.firebasestorage.app",
  messagingSenderId: "648033572781",
  appId: "1:648033572781:web:6bc6b893c357391e263324",
  measurementId: "G-K9LD75XNEG"
};

const app = initializeApp(firebaseConfig);
const analytics = getAnalytics(app);
const auth = getAuth(app);
const googleProvider = new GoogleAuthProvider();

export { auth, googleProvider, analytics };
