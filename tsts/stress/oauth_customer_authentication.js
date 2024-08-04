import http from 'k6/http';

export const options = {
    vus: 50,
    duration: '10s',
    thresholds: {
        http_req_failed: ['rate<0.01'], // http errors should be less than 1%
        http_req_duration: ['p(100)<100'], // 100% das requisições precisam menor que 100ms
    },
};

export default function () {
    const payload = {
        grantType: 'password',
        email: 'otaviovb.developer@gmail.com',
        password: '_836hjfk7DH8!'
    };

    const urlEncodedPayload = Object.keys(payload)
        .map(key => `${encodeURIComponent(key)}=${encodeURIComponent(payload[key])}`)
        .join('&');


    http.post('http://localhost:5001/api/v1/customers/oauth/token', urlEncodedPayload, {
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        }
    });
}