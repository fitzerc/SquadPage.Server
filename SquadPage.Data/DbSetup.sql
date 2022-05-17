--create squad table
CREATE TABLE IF NOT EXISTS public.squad
(
    "squad_name" text NOT NULL,
    "squad_id" serial NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 ),
    "squad_number" integer NOT NULL,
    PRIMARY KEY ("squad_id")
);

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