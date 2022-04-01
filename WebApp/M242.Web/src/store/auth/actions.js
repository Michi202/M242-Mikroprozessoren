export const login = (context, obj) => {
    context.commit("setJWT", obj.jwt);
    console.log(obj.jwt)
    axios.defaults.headers.common['authorization'] = "Bearer " + obj.jwt;
};
export const logout = (context) => {
    context.commit("setJWT", null);
    axios.defaults.headers.common['authorization'] = null;
};




