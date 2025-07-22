--
-- PostgreSQL database dump
--

-- Dumped from database version 16.4
-- Dumped by pg_dump version 16.4

-- Started on 2025-07-23 01:00:31

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 215 (class 1259 OID 57582)
-- Name: companies; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.companies (
    id bigint NOT NULL,
    name character varying(100) NOT NULL
);


ALTER TABLE public.companies OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 57587)
-- Name: departments; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.departments (
    id bigint NOT NULL,
    name character varying(100) NOT NULL,
    phone character varying(55) NOT NULL,
    company_id bigint NOT NULL
);


ALTER TABLE public.departments OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 57597)
-- Name: employees; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.employees (
    id bigint NOT NULL,
    name character varying(55) NOT NULL,
    surname character varying(55) NOT NULL,
    phone character varying(55) NOT NULL,
    company_id integer NOT NULL,
    department_id integer NOT NULL
);


ALTER TABLE public.employees OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 57612)
-- Name: passports; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.passports (
    employee_id bigint NOT NULL,
    type character varying(50) NOT NULL,
    number character varying(50) NOT NULL
);


ALTER TABLE public.passports OWNER TO postgres;

--
-- TOC entry 4800 (class 0 OID 57582)
-- Dependencies: 215
-- Data for Name: companies; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.companies (id, name) FROM stdin;
1	TestCorp
2	TestCorp2
3	TestCorp3
\.


--
-- TOC entry 4801 (class 0 OID 57587)
-- Dependencies: 216
-- Data for Name: departments; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.departments (id, name, phone, company_id) FROM stdin;
1	Разработка	+7(999)999-99-99	1
2	Отдел кадров	+7(999)999-99-99	1
3	HR-Отдел	+7(999)999-99-99	3
\.


--
-- TOC entry 4802 (class 0 OID 57597)
-- Dependencies: 217
-- Data for Name: employees; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.employees (id, name, surname, phone, company_id, department_id) FROM stdin;
5880730948503600243	Джеф	Безос	+7(000)000-00-00	3	3
-5934547905675577670	Петр	Афанасьев	+7(922)222-22-22	1	1
1057143786262611481	Петр	Афанасьев	+7(933)333-33-33	1	2
6087795395501691414	Федор	Петров	+7(944)444-44-44	1	3
-6283816443707238103	Алексей	Павлов	+7(955)555-55-55	2	1
-5338220924911280017	Никита	Алексеев	+7(966)666-66-66	2	2
-7972001006124668889	Никита	Никитов	+7(977)777-77-77	2	3
-7489349760026832219	Вениамин	Маск	+7(988)888-88-88	3	1
4887198824721031052	Илон	Маск	+7(999)999-99-99	3	2
\.


--
-- TOC entry 4803 (class 0 OID 57612)
-- Dependencies: 218
-- Data for Name: passports; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.passports (employee_id, type, number) FROM stdin;
-5934547905675577670	Служебный	1111 111111
1057143786262611481	Служебный	2222 222222
6087795395501691414	Внутренний	4444 444444
-6283816443707238103	Внутренний	5555 555555
-5338220924911280017	Внутренний	6666 666666
-7972001006124668889	Внутренний	7777 777777
-7489349760026832219	Заграничный	8888 888888
4887198824721031052	Внеземной	999999 999999
5880730948503600243	Земной	0000 000000
\.


--
-- TOC entry 4646 (class 2606 OID 57586)
-- Name: companies companies_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.companies
    ADD CONSTRAINT companies_pkey PRIMARY KEY (id);


--
-- TOC entry 4648 (class 2606 OID 57591)
-- Name: departments departments_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.departments
    ADD CONSTRAINT departments_pkey PRIMARY KEY (id);


--
-- TOC entry 4650 (class 2606 OID 57601)
-- Name: employees employees_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.employees
    ADD CONSTRAINT employees_pkey PRIMARY KEY (id);


--
-- TOC entry 4652 (class 2606 OID 57616)
-- Name: passports passports_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.passports
    ADD CONSTRAINT passports_pkey PRIMARY KEY (employee_id);


--
-- TOC entry 4653 (class 2606 OID 57592)
-- Name: departments departments_company_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.departments
    ADD CONSTRAINT departments_company_id_fkey FOREIGN KEY (company_id) REFERENCES public.companies(id);


--
-- TOC entry 4654 (class 2606 OID 57602)
-- Name: employees employees_company_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.employees
    ADD CONSTRAINT employees_company_id_fkey FOREIGN KEY (company_id) REFERENCES public.companies(id);


--
-- TOC entry 4655 (class 2606 OID 57607)
-- Name: employees employees_department_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.employees
    ADD CONSTRAINT employees_department_id_fkey FOREIGN KEY (department_id) REFERENCES public.departments(id);


--
-- TOC entry 4656 (class 2606 OID 57622)
-- Name: passports passports_employee_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.passports
    ADD CONSTRAINT passports_employee_id_fkey FOREIGN KEY (employee_id) REFERENCES public.employees(id) ON DELETE CASCADE;


-- Completed on 2025-07-23 01:00:32

--
-- PostgreSQL database dump complete
--

