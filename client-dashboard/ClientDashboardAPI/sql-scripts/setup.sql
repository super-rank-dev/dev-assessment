-- create the clients table
CREATE TABLE ClientDashboardDB.clients (
    id SERIAL PRIMARY KEY,
    first_name VARCHAR(255),
    last_name VARCHAR(255),
    archived BOOLEAN DEFAULT false
);

-- create the phone_numbers table
CREATE TABLE ClientDashboardDB.phone_numbers (
    id SERIAL PRIMARY KEY, 
    phone_number VARCHAR(20)
);

-- create the clients_phone_numbers table
CREATE TABLE ClientDashboardDB.clients_phone_numbers (
    client_id INTEGER,
    phone_number_id INTEGER
);