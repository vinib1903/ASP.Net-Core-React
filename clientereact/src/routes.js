import React from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Login from './pages/Login';
import Alunos from './pages/Alunos';
import NovoAluno from './pages/NovoAluno';
import Acesso from './pages/Acesso';

export default function Router() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" exact element={<Login />} />
                <Route path="/alunos" element={<Alunos />} />
                <Route path="/alunos/novo/:alunoId" element={<NovoAluno />} />
                <Route path="/alunos/acesso" element={<Acesso />} />
            </Routes>
        </BrowserRouter>
    );
}
