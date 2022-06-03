async function getall() {
    const r = await fetch('/api/contacts');
    const d = await r.json();
    console.log(d);
}
async function get() {
    const r = await fetch('/api/contacts/:3/messages/:2');
    const d = await r.json();
    console.log(d);
}
async function put() {
    const r = await fetch('/api/contacts/:3/messages/:2', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ content: 'ddd' })
    });
    console.log(r);
}
async function post() {
    const r = await fetch('/api/contacts/transfer', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({server :'helloworld',from:'safwan',to:'mramhot'})
    });
    console.log(r);
}
async function Delete() {
    const r = await fetch('/api/contacts/:3/messages/:2', {
        method: 'DELETE'
        
    });
    console.log(r);
}