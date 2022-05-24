--create squad table
CREATE TABLE IF NOT EXISTS public.squad
(
    "squad_name" text NOT NULL,
    "squad_id" serial NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 ),
    "squad_number" integer NOT NULL,
    PRIMARY KEY ("squad_id")
);

--add squad data for our team and team's we play
INSERT INTO squad (squad_name, squad_number)
VALUES ('Setsaholics Anonymous', 12);

INSERT INTO squad (squad_name, squad_number)
VALUES ('Aceholes', 11);

INSERT INTO squad (squad_name, squad_number)
VALUES ('Amateur Hour', 13);

INSERT INTO squad (squad_name, squad_number)
VALUES ('For the Gram', 10);

INSERT INTO squad (squad_name, squad_number)
VALUES ('Set Alcholics', 20);

INSERT INTO squad (squad_name, squad_number)
VALUES ('Block Heads', 15);

INSERT INTO squad (squad_name, squad_number)
VALUES ('Sugar and Spice', 16);

INSERT INTO squad (squad_name, squad_number)
VALUES ('Wilson Whalers', 24);

INSERT INTO squad (squad_name, squad_number)
VALUES ('Thats what she set', 6);

INSERT INTO squad (squad_name, squad_number)
VALUES ('Spiked Punch', 9);

INSERT INTO squad (squad_name, squad_number)
VALUES ('Blue Balls', 14);

INSERT INTO squad (squad_name, squad_number)
VALUES ('Spike Tysons', 5);

--create game_day
CREATE TABLE IF NOT EXISTS public.game_day
(
    "game_day_id" serial NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 ),
    "game_date" timestamp,
    "game_location" text,
    "home_squad_id" integer,
    "away_squad_id" integer,
    "game_status" text,
    "game_type" text NOT NULL,
    PRIMARY KEY ("game_day_id")
);

--game_day data
INSERT INTO game_day (game_date, game_location, home_squad_id, away_squad_id, game_status, game_type)
VALUES (
  '2022-05-18 20:05:00-00',
  'Court 5',
  (SELECT squad_id FROM squad WHERE squad_number = 12),
  (SELECT squad_id FROM squad WHERE squad_number = 11),
  'Upcoming',
  'Regular'
);

INSERT INTO game_day (game_date, game_location, home_squad_id, away_squad_id, game_status, game_type)
VALUES (
  '2022-05-25 19:15:00-00',
  'Court 5',
  (SELECT squad_id FROM squad WHERE squad_number = 13),
  (SELECT squad_id FROM squad WHERE squad_number = 12),
  'Upcoming',
  'Regular'
);

INSERT INTO game_day (game_date, game_location, home_squad_id, away_squad_id, game_status, game_type)
VALUES (
  '2022-06-01 20:55:00-00',
  'Court 2',
  (SELECT squad_id FROM squad WHERE squad_number = 12),
  (SELECT squad_id FROM squad WHERE squad_number = 10),
  'Upcoming',
  'Regular'
);

INSERT INTO game_day (game_date, game_location, home_squad_id, game_status, game_type)
VALUES (
  '2022-06-15 08:25:00-00',
  'Regular',
  (SELECT squad_id FROM squad WHERE squad_number = 12),
  'Upcoming',
  'Bye'
);

INSERT INTO game_day (game_date, game_location, home_squad_id, away_squad_id, game_status, game_type)
VALUES (
  '2022-06-15 18:25:00-00',
  'Court 3',
  (SELECT squad_id FROM squad WHERE squad_number = 20),
  (SELECT squad_id FROM squad WHERE squad_number = 12),
  'Upcoming',
  'Regular'
);

INSERT INTO game_day (game_date, game_location, home_squad_id, away_squad_id, game_status, game_type)
VALUES (
  '2022-06-22 18:25:00-00',
  'Court 4',
  (SELECT squad_id FROM squad WHERE squad_number = 15),
  (SELECT squad_id FROM squad WHERE squad_number = 12),
  'Upcoming',
  'Regular'
);

INSERT INTO game_day (game_date, game_location, home_squad_id, away_squad_id, game_status, game_type)
VALUES (
  '2022-06-29 20:05:00-00',
  'Court 1',
  (SELECT squad_id FROM squad WHERE squad_number = 16),
  (SELECT squad_id FROM squad WHERE squad_number = 12),
  'Upcoming',
  'Regular'
);

INSERT INTO game_day (game_date, game_location, home_squad_id, away_squad_id, game_status, game_type)
VALUES (
  '2022-07-06 20:55:00-00',
  'Court 3',
  (SELECT squad_id FROM squad WHERE squad_number = 24),
  (SELECT squad_id FROM squad WHERE squad_number = 12),
  'Upcoming',
  'Regular'
);

INSERT INTO game_day (game_date, game_location, home_squad_id, away_squad_id, game_status, game_type)
VALUES (
  '2022-07-13 19:15:00-00',
  'Court 4',
  (SELECT squad_id FROM squad WHERE squad_number = 12),
  (SELECT squad_id FROM squad WHERE squad_number = 6),
  'Upcoming',
  'Regular'
);

INSERT INTO game_day (game_date, game_location, home_squad_id, away_squad_id, game_status, game_type)
VALUES (
  '2022-07-20 19:15:00-00',
  'Court 2',
  (SELECT squad_id FROM squad WHERE squad_number = 12),
  (SELECT squad_id FROM squad WHERE squad_number = 9),
  'Upcoming',
  'Regular'
);

INSERT INTO game_day (game_date, game_location, home_squad_id, away_squad_id, game_status, game_type)
VALUES (
  '2022-07-27 20:55:00-00',
  'Court 5',
  (SELECT squad_id FROM squad WHERE squad_number = 14),
  (SELECT squad_id FROM squad WHERE squad_number = 12),
  'Upcoming',
  'Regular'
);

INSERT INTO game_day (game_date, game_location, home_squad_id, away_squad_id, game_status, game_type)
VALUES (
  '2022-08-03 19:15:00-00',
  'Court 5',
  (SELECT squad_id FROM squad WHERE squad_number = 12),
  (SELECT squad_id FROM squad WHERE squad_number = 5),
  'Upcoming',
  'Regular'
);

--create match_results
CREATE TABLE IF NOT EXISTS public.match_results
(
    "match_results_id" serial NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 ),
    "game_day_id" integer NOT NULL,
    PRIMARY KEY ("match_results_id")
);

--create game
CREATE TABLE IF NOT EXISTS public.game_results
(
    "game_results_id" serial NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 ),
    "match_results_id" integer NOT NULL,
    "home_score" integer NOT NULL,
    "away_score" integer NOT NULL,
    PRIMARY KEY ("game_results_id")
);

--create person
CREATE TABLE IF NOT EXISTS public.person
(
    "person_id" serial NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 ),
    "person_name" text Not NULL,
    "user_name" text,
    PRIMARY KEY ("person_id")
);

--create squad_member
CREATE TABLE IF NOT EXISTS public.squad_member
(
    "squad_member_id" serial NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 ),
    "squad_id" integer Not NULL,
    "person_id" integer NOT NULL,
    PRIMARY KEY ("squad_member_id")
);