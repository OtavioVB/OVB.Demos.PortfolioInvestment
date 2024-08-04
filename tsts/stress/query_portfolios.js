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
    http.get('http://localhost:5001/api/v1/financial-assets/portfolios?page=1&size=25', {
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiQ3VzdG9tZXIiLCJDdXN0b21lcklkIjoiZWQxZDcyMWQtNjJiYS00NGVmLWI4ODctMGMwYjgxMmVkMzdkIiwiR3JhbnRUeXBlIjoicGFzc3dvcmQiLCJDb2RlIjoiQ1NUOTRLREpOWDMiLCJFbWFpbCI6Im90YXZpb3ZiLmRldmVsb3BlckBnbWFpbC5jb20iLCJuYmYiOjE3MjI3ODkxMzAsImV4cCI6MTcyMjgxNzkzMCwiaWF0IjoxNzIyNzg5MTMwfQ.qoZplbKxdNbL3eUjV1d1QDslLaVitwHzLMhxMlmMWxw'
        }
    });
}