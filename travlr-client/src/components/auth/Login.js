import React, { useState } from "react"
import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom"
import { LoginUser } from "../API/LoginAPI";


export const Login = () => {
    const [email, setEmail] = useState("")
    const [firebaseId, setPass] = useState("")
    const navigate = useNavigate()


    const AuthenticatingUser = async () => {
      let LoginData = await LoginUser(firebaseId);

      if(LoginData === false ){
          console.log("Incorrect Data")
      } else {
        console.log("success")
        localStorage.setItem("project_user", JSON.stringify({
                               userId: LoginData.id,
                           }))

        navigate("/")
      }

  }

    return (
        <>
            <head>
            <link rel="stylesheet" href="index.css"></link>
            </head>
            <body className="flex justify-center items-center h-screen">              
            <div className="flex flex-col items-center">
                <div className="mb-6">
                  <label htmlFor="email" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Email address</label>
                  <input type="email" id="email" 
                  className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-96 p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" 
                  placeholder="john.doe@company.com"  
                  onChange={evt => setEmail(evt.target.value)}
                  required autoFocus
                  />
                </div> 
                <div className="mb-6">
                  <label htmlFor="password" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Password</label>
                  <input type="password" id="password" 
                  className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-96 p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" 
                  placeholder="" 
                  onChange={evt => setPass(evt.target.value)}
                  required />
                </div>
                <div className="flex items-start mb-6">
                </div>
                <button
            type="submit"
            className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
            onClick={() => AuthenticatingUser()}
          >
            Submit
          </button>
          <section className="mt-4">
            <Link className="fire" to="/register">
              Not a member yet?
            </Link>
          </section>
        </div>
      </body>
    </>
  );
}      