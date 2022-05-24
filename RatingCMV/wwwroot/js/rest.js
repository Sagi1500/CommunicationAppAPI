//Registers:



//Contacts:
async function getAllC() {
    const r = await fetch('/api/Contacts');
    const d = await r.json();
    console.log(d);
}
async function getC() {
    const r = await fetch('/api/Contacts/a1');
    const d = await r.json();
    console.log(d);
}
async function postC() {
    const r = await fetch('/api/Contacts', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ id: 'cc', name: 'cc', server: 'cc' })
    });
    console.log(r);
}

async function putC() {
    const r = await fetch('/api/Contacts/a1', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(['b2', 'server2'])
    });
    console.log(r);
}
async function delC() {
    const r = await fetch('/api/Contacts/a1', {
        method: 'DELETE'
    });
    console.log(r);
}


//Messages:
async function getAllM() {
    const r = await fetch('/api/Contacts/a1/messages');
    const d = await r.json();
    console.log(d);
}
async function getM() {
    const r = await fetch('/api/Contacts/a1/messages/1');
    const d = await r.json();
    console.log(d);
}
async function postM() {
    const r = await fetch('/api/Contacts/a1/messages', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify('Hello')
    });
    console.log(r);
}

async function putM() {
    const r = await fetch('/api/Contacts/a1/messages/1', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify('Hi')
    });
    console.log(r);
}
async function delM() {
    const r = await fetch('/api/Contacts/a1/messages/1', {
        method: 'DELETE'
    });
    console.log(r);
}
