import React, { useState, useEffect } from 'react';
import api from '../../services/api';
import defaultImage from '../../assets/defaultImage.png';
import './styles.css';
import { Link, useNavigate } from 'react-router-dom';

const Acesso = () => {
    const [senha, setSenha] = useState('');
    const [aluno, setAluno] = useState(null);
    const [mensagem, setMensagem] = useState('');
    const [darkMode, setDarkMode] = useState(false);
    const navigate = useNavigate();

    const email = localStorage.getItem('email');
    const token = localStorage.getItem('token');

    const authorization = {
        headers: {
            Authorization: `Bearer ${token}`
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
        <div className={`acesso-container ${darkMode ? 'dark-theme' : ''}`}>
            <div className="theme-toggle">
                <input type="checkbox" id="dark-mode-toggle" checked={darkMode} onChange={() => setDarkMode(!darkMode)} />
                <label htmlFor="dark-mode-toggle"></label>
            </div>

            <h2>Digite sua senha</h2>
            <div className="aluno-foto">
                <img src={aluno ? aluno.foto || defaultImage : defaultImage} alt="Foto do Aluno" className="aluno-foto-img" />
            </div>
            <input type="password" value={senha} onChange={(e) => setSenha(e.target.value)} />
            <button className={`acessar-button ${darkMode ? 'dark-theme' : ''}`} onClick={handleAcesso}>
                Acessar
            </button>

            <div className="aluno-info">
                {aluno && (
                    <p>Olá {aluno.nome}, tenha um bom treino!</p>
                )}
                {!aluno && <div className="placeholder"></div>}
            </div>

            {mensagem && (
                <div className="error-message">
                    <p>{mensagem}</p>
                </div>
            )}
            {!mensagem && <div className="placeholder"></div>}

            <button className={`return-button ${darkMode ? 'dark-theme' : ''}`} onClick={() => navigate('/alunos')}>
                Retornar
            </button>
        </div>
    );
}

export default Acesso;
