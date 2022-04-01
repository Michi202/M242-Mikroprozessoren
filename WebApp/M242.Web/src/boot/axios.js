import { boot } from 'quasar/wrappers'
import axios from 'axios'
import store from './../store'


export default boot(({ app, store }) => {
  // for use inside Vue files (Options API) through this.$axios and this.$api
  const instance = axios.create({
    baseURL: process.env.API_URL,
    headers: {
      common: {
        'authorization': 'Bearer ' + store.getters["auth/JWT"]
      }
    }
  })
  instance.interceptors.response.use(
    response => {
      if (response.status == 500) {
        Notify.create({
          message: "Internal Server error!",
          type: "negative",
          position: "top"
        });
      }
      return response;
    },
    error => {
      let response = error.response;

      if (response.status == 403 || response.status == 401) {
        Notify.create({
          message: "Not allowed!",
          type: "negative",
          position: "top"
        });
      }
      if (response) {
        return response;
      } else {
        return error;
      }
    }
  );

  window.axios = instance;
  app.config.globalProperties.$axios = instance
  // ^ ^ ^ this will allow you to use this.$axios (for Vue Options API form)
  //       so you won't necessarily have to import axios in each vue file

  app.config.globalProperties.$api = instance
  // ^ ^ ^ this will allow you to use this.$api (for Vue Options API form)
  //       so you can easily perform requests against your app's API
})

export { }
