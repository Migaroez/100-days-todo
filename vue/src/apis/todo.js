import axios from "axios";

export default () => {
  var client = axios.create({
    baseURL: process.env.VUE_APP_API_TODO,
    withCredentials: false,
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
  });

  return client;
};
