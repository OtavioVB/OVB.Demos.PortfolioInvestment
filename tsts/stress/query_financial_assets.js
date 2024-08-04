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
    http.get('http://localhost:5001/api/v1/financial-assets', {
        headers: {
            'Content-Type': 'application/json'
        }
    });
}