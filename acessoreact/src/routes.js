import React from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Login from './pages/Login';
import Acesso from './pages/Acesso';

export default function Router() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" exact element={<Login />} />
                <Route path="/acesso" exact element={<Acesso />} />
            </Routes>
        </BrowserRouter>
    );
}
