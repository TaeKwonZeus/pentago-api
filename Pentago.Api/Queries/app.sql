CREATE TABLE IF NOT EXISTS users
(
    id                  TEXT PRIMARY KEY,
    username            TEXT NOT NULL,
    normalized_username TEXT NOT NULL UNIQUE,
    email               TEXT NOT NULL UNIQUE,
    password_hash       TEXT NOT NULL,
    api_key_hash        TEXT NOT NULL,
    glicko_rating       INT  NOT NULL,
    glicko_rd           INT  NOT NULL
);

CREATE TABLE IF NOT EXISTS games
(
    id    TEXT PRIMARY KEY,
    white TEXT NOT NULL,
    black TEXT NOT NULL,
    FOREIGN KEY (white) REFERENCES users (id),
    FOREIGN KEY (black) REFERENCES users (id)
);

CREATE TABLE IF NOT EXISTS moves
(
    id               TEXT PRIMARY KEY,
    game_id          TEXT NOT NULL,
    previous_move_id TEXT,
    move_number      INT  NOT NULL,
    move             TEXT NOT NULL,
    FOREIGN KEY (game_id) REFERENCES games (id),
    FOREIGN KEY (previous_move_id) REFERENCES moves (id)
);