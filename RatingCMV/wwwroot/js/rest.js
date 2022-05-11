async function getAll() {
    const r = await fetch('/api/Users2');
    const d = await r.json();
    console.log(d);
}
async function get() {
    const r = await fetch('/api/Users2/1');
    const d = await r.json();
    console.log(d);
}
async function post() {
    const r = await fetch('/api/Users2', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ id: username, password: password })
    });
    console.log(r);
}