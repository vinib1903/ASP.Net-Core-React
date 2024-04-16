import React, { useState, useEffect } from 'react';
import api from '../../services/api';
import defaultImage from '../../assets/defaultImage.png';
import './styles.css';
import {Link, useNavigate} from 'react-router-dom';

export default function Acesso() {
    const [senha, setSenha] = useState('');
    const [aluno, setAluno] = useState(null);
    const [mensagem, setMensagem] = useState('');

    const email = localStorage.getItem('email');
    const token = localStorage.getItem('token');
  
    const authorization = {
        headers : {
          Authorization : `Bearer ${token}`
        }
    }

    useEffect(() => {
        if (aluno) {
            const timer = setTimeout(() => {
                setAluno(null);
            }, 3000);

            return () => clearTimeout(timer);
        }
    }, [aluno]);

    async function handleAcesso() {
        setAluno(null);
        setSenha('');
        setMensagem('');

        try {
            const response = await api.get(`/api/Alunos/${senha}/acesso`, authorization);
            setAluno(response.data);
        } catch (error) {
            setMensagem('Senha inválida');
        }
    }

    return (
        <div className="acesso-container">
            <h2>Digite sua senha</h2>
            <div className="aluno-foto">
                <img src={aluno ? aluno.foto || defaultImage : defaultImage} alt="Foto do Aluno" className="aluno-foto-img" />
            </div>
            <input type="password" value={senha} onChange={(e) => setSenha(e.target.value)} />
            <button onClick={handleAcesso}>Acessar</button>
            {aluno && (
                <div className="aluno-info">
                    <p>Olá {aluno.nome}, tenha um bom treino!</p>
                </div>
            )}
            {mensagem && <p className="error-message">{mensagem}</p>}

            <Link className="" to="/alunos" height="30px" width="100%" >Retornar</Link>
        </div>

        
    );
}
