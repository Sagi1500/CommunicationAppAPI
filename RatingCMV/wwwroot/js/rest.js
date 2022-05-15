async function getAll() {
    const r = await fetch('/api/Contacts');
    const d = await r.json();
    console.log(d);
}
async function get() {
    const r = await fetch('/api/Contacts/a11');
    const d = await r.json();
    console.log(d);
}
async function post() {
    const r = await fetch('/api/Contacts', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ id: 'a1', name: 'a1', server:'a1'})
    });
    console.log(r);
}
async function put() {
    const r = await fetch('/api/Contacts', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ id: 'a1', name: 'a11', server: 'a11' })
    });
    console.log(r);
}
async function del() {
    const r = await fetch('/api/Contacts/a1', {
        method: 'DELETE'   
    });
    console.log(r);
}
