import React, {createContext, ReactNode, useState} from "react";

export const LoginContext = createContext({
    isLoggedIn: false,
    isAdmin: false,
    logIn: (userName: string,password:string) => {},
    logOut: () => {},
    UserName: "",
    Password: ""
});

interface LoginManagerProps {
    children: ReactNode
}

export function LoginManager(props: LoginManagerProps): JSX.Element {
    const [loggedIn, setLoggedIn] = useState(true);
    const [UserName, setUserName] = useState("");
    const [Password, setPassword] = useState("");

    function logIn(userName: string, password:string) {
        setLoggedIn(true);
        setUserName(userName);
        setPassword(password);
    }
    
    function logOut() {
        setLoggedIn(false);
    }
    
    const context = {
        isLoggedIn: loggedIn,
        isAdmin: loggedIn,
        logIn: logIn,
        logOut: logOut,        
        UserName: UserName,
        Password: Password
    };
    
    return (
        <LoginContext.Provider value={context}>
            {props.children}
        </LoginContext.Provider>
    );
}