const express = require('express');
const bodyParser = require('body-parser');
const generateHairstyle = require('./api/hairstyle');
const path = require('path');
require('dotenv').config();
import fetch from 'node-fetch';  // ES6 import

const app = express();
const port = 3000;

// EJS template engine kullanıyoruz
app.set('view engine', 'ejs');
app.use(express.static(path.join(__dirname, 'public')));
app.use(bodyParser.urlencoded({ extended: true }));

// Ana sayfa: sadece "Saç Önerisi Al" butonu
app.get('/', (req, res) => {
    res.render('index');
});

// Saç önerisi sayfası
app.get('/hairstyle', (req, res) => {
    res.render('hairstyle');
});

// Saç önerisi formu gönderildiğinde işleme
app.post('/hairstyle', async (req, res) => {
    const { imageUrl, textPrompt } = req.body;

    try {
        // API'yi çağırarak sonucu alıyoruz
        const outputImage = await generateHairstyle(imageUrl, textPrompt);
        res.render('hairstyle', { imageUrl: outputImage });
    } catch (error) {
        console.error("Error generating hairstyle:", error);
        res.render('hairstyle', { imageUrl: null, error: 'Failed to generate hairstyle' });
    }
});

// Sunucuyu başlat
app.listen(port, () => {
    console.log(`Server is running at http://localhost:${port}`);
});
