
import * as React from "react";
import * as ReactDOM from "react-dom";
import { BrowserRouter } from "react-router-dom"

import { Layout } from './components/Layout';

ReactDOM.render(
    <BrowserRouter>
        <Layout />
    </BrowserRouter>
    ,
    document.getElementById("WebBlogApplication")

);