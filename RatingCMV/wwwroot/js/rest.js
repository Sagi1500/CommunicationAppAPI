async function getAll() {
    const r = await fetch('/api/Users2');
    const d = await r.json();
    console.log(d);
}
async function get() {
    const r = await fetch('/api/Users2/a');
    const d = await r.json();
    console.log(d);
}
async function post() {
    const r = await fetch('/api/Users2', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ id: 'a1', password: 'a1'})
    });
    console.log(r);
}