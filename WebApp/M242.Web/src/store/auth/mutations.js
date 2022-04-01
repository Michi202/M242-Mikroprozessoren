import Vue from 'vue'

export const setJWT = (state, jwt) => {
    state.jwt = jwt;
    state.authorized = jwt != '' && jwt != null && jwt != undefined;
};
