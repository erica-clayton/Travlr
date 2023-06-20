const GetConfig = {
    credentials: "include",
    method: "GET",
    headers: { "Content-Type": "application/json" },
  }

const GetPostConfig = (body) => {
    var config = {
        body : JSON.stringify(body),
        credentials: "include",
        method: "POST",
        headers: { "Content-Type": "application/json" },
    }
    return config
}

export const Register = async(Name, DateCreated, Email, FirebaseId) => {

    const response = await fetch(
        `https://localhost:7129/AddUser`, GetPostConfig({Name,DateCreated,Email,FirebaseId})
        );

    if(response.ok){
        const RegisterResponse = await response.json();
        return RegisterResponse;
    }else {
        return false
    }    
}


export const LoginUser = async(uid) => {

    const response = await fetch(`https://localhost:7129/Users/uid/${uid}`)
    const user = await response.json();
    return user
    
}


const API = {
    LoginUser,
    Register
}

export default API;