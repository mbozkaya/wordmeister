import React, { useState } from "react";
import AnimationRevealPage from "./../helpers/AnimationRevealPage.js";
import { Container as ContainerBase } from "./../components/misc/Layouts";
import tw from "twin.macro";
import styled from "styled-components";
import { css } from "styled-components/macro"; //eslint-disable-line
import illustration from "../images/signup-illustration.svg";
import logo from "../images/logo.svg";
import googleIconImageSrc from "../images/google-icon.png";
import twitterIconImageSrc from "../images/twitter-icon.png";
import { ReactComponent as SignUpIcon } from "feather-icons/dist/icons/user-plus.svg";
import { Link } from "react-router-dom";
import accountService from '../services/accountService';
import { AuthContext } from "../contexts/authContext.js";
import { Redirect } from "react-router-dom";

const Container = tw(ContainerBase)
  `min-h-screen bg-primary-900 text-white font-medium flex justify-center -m-8`;
const Content = tw.div`max-w-screen-xl m-0 sm:mx-20 sm:my-16 bg-white text-gray-900 shadow sm:rounded-lg flex justify-center flex-1`;
const MainContainer = tw.div`lg:w-1/2 xl:w-5/12 p-6 sm:p-12`;
const LogoLink = tw.a``;
const LogoImage = tw.img`h-12 mx-auto`;
const MainContent = tw.div`mt-12 flex flex-col items-center`;
const Heading = tw.h1`text-2xl xl:text-3xl font-extrabold`;
const FormContainer = tw.div`w-full flex-1 mt-8`;

const SocialButtonsContainer = tw.div`flex flex-col items-center`;
const SocialButton = styled.a`
  ${tw`w-full max-w-xs font-semibold rounded-lg py-3 border text-gray-900 bg-gray-100 hocus:bg-gray-200 hocus:border-gray-400 flex items-center justify-center transition-all duration-300 focus:outline-none focus:shadow-outline text-sm mt-5 first:mt-0`}
  .iconContainer {
    ${tw`bg-white p-2 rounded-full`}
  }
  .icon {
    ${tw`w-4`}
  }
  .text {
    ${tw`ml-4`}
  }
`;

const DividerTextContainer = tw.div`my-12 border-b text-center relative`;
const DividerText = tw.div`leading-none px-2 inline-block text-sm text-gray-600 tracking-wide font-medium bg-white transform -translate-y-1/2 absolute inset-x-0 top-1/2 bg-transparent`;

const Form = tw.form`mx-auto max-w-xs`;
const Input = tw.input`w-full px-8 py-4 rounded-lg font-medium bg-gray-100 border border-gray-200 placeholder-gray-500 text-sm focus:outline-none focus:border-gray-400 focus:bg-white mt-5 first:mt-0`;
const SubmitButton = styled.button`
  ${tw`mt-5 tracking-wide font-semibold bg-primary-500 text-gray-100 w-full py-4 rounded-lg hover:bg-primary-900 transition-all duration-300 ease-in-out flex items-center justify-center focus:shadow-outline focus:outline-none`}
  .icon {
    ${tw`w-6 h-6 -ml-2`}
  }
  .text {
    ${tw`ml-3`}
  }
`;
const IllustrationContainer = tw.div`sm:rounded-r-lg flex-1 bg-purple-100 text-center hidden lg:flex justify-center`;
const IllustrationImage = styled.div`
  ${props => `background-image: url("${props.imageSrc}");`}
  ${tw`m-12 xl:m-16 w-full max-w-lg bg-contain bg-center bg-no-repeat`}
`;

export default ({
  logoLinkUrl = "/",
  illustrationImageSrc = illustration,
  headingText = "Sign Up For Treact",
  socialButtons = [
    {
      iconImageSrc: googleIconImageSrc,
      text: "Sign Up With Google",
      url: "https://google.com"
    },
    {
      iconImageSrc: twitterIconImageSrc,
      text: "Sign Up With Twitter",
      url: "https://twitter.com"
    }
  ],
  submitButtonText = "Sign Up",
  SubmitButtonIcon = SignUpIcon,
  tosUrl = "#",
  privacyPolicyUrl = "#",
  signInUrl = "login"
}) => {

  const [form, setForm] = useState({
    email: '',
    emailError: true,
    password: '',
    rePassword: '',
    passwordError: true,
    firstname: 'Muhammet',
    lastname: 'Bozkaya',
    privacyPolicyCheck: false,
  });

  const handleSingUp = (e) => {
    if (form.emailError || form.passwordError) {
      alert('Please Enter valid password and email');
      return null;
    }

    const model = {
      firstname: form.firstname,
      lastname: form.lastname,
      email: form.email,
      password: form.password,
    };
    return model;
  }

  return (
    <AuthContext.Consumer>
      {({ authorize, signupError, signupErrorMessage, onSignup }) =>
        authorize ? <Redirect to="/Dashboard" />
          :
          (
            <AnimationRevealPage>
              <Container>
                <Content>
                  <MainContainer>
                    <Link to={logoLinkUrl}>
                      <LogoImage src={logo} />
                    </Link>
                    <MainContent>
                      <Heading>{headingText}</Heading>
                      <FormContainer>
                        {/* <SocialButtonsContainer>
                  {socialButtons.map((socialButton, index) => (
                    <SocialButton key={index} href={socialButton.url}>
                      <span className="iconContainer">
                        <img src={socialButton.iconImageSrc} className="icon" alt="" />
                      </span>
                      <span className="text">{socialButton.text}</span>
                    </SocialButton>
                  ))}
                </SocialButtonsContainer>
                <DividerTextContainer>
                  <p style={{ padding: '10px' }}>Or Sign in with your e-mail</p>
                </DividerTextContainer> */}

                        <Form onKeyDown={e => {
                          if (e.key.toLowerCase() === 'enter' && form.email !== '' && !form.emailError && !form.passwordError) {
                            const model = handleSingUp();
                            onSignup(model);
                          }
                        }}>
                          <Input
                            type="email"
                            placeholder="Email"
                            value={form.email}
                            onChange={val => setForm({
                              ...form,
                              email: val.target.value,
                            })}
                            onBlur={e => {
                              setForm({
                                ...form,
                                emailError: !/^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$/.test(e.target.value.toLowerCase()),
                              });
                            }}
                          />
                          {form.email !== '' && form.emailError && <div className="alert alert-danger">Please enter valid Email</div>}
                          <Input
                            type="password"
                            placeholder="Password"
                            value={form.password}
                            onChange={event => setForm({
                              ...form,
                              password: event.target.value,
                              passwordError: event.target.value !== form.rePassword,
                            })} />
                          {form.password !== '' && form.rePassword !== '' && form.passwordError && <div className="alert alert-danger">Password and Re Password must be match</div>}
                          <Input
                            type="password"
                            placeholder="Re Password"
                            onChange={event => setForm({
                              ...form,
                              rePassword: event.target.value,
                              passwordError: event.target.value !== form.password
                            })}
                            value={form.rePassword}
                          />
                          {form.password !== '' && form.rePassword !== '' && form.passwordError && <div className="alert alert-danger">Password and Re Password must be match</div>}
                          <div style={{ paddingTop: '10px' }}>
                            <input type='checkbox' style={{
                              display: 'inline',
                              marginRight: '20px'
                            }} checked={form.privacyPolicyCheck} onChange={event => setForm({
                              ...form,
                              privacyPolicyCheck: event.target.checked,
                            })} />

                            <p tw="mt-6 text-xs text-gray-600 text-center" style={{ display: "inline" }}>
                              I agree to abide by treact's{" "}
                              <a href={tosUrl} tw="border-b border-gray-500 border-dotted">
                                Terms of Service
                    </a>{" "}
                    and its{" "}
                              <a href={privacyPolicyUrl} tw="border-b border-gray-500 border-dotted">
                                Privacy Policy
                    </a>
                            </p>
                          </div>
                          {signupError && <div className="alert alert-danger">{signupErrorMessage}</div>}

                          <SubmitButton type="button" onClick={e => {
                            const model = handleSingUp()
                            onSignup(model)
                          }}>

                            <SubmitButtonIcon className="icon" />
                            <span className="text">{submitButtonText}</span>
                          </SubmitButton>
                          <p tw="mt-8 text-sm text-gray-600 text-center">
                            Already have an account?{" "}
                            <a href={signInUrl} tw="border-b border-gray-500 border-dotted">
                              Sign In
                    </a>
                          </p>
                        </Form>
                      </FormContainer>
                    </MainContent>
                  </MainContainer>
                  <IllustrationContainer>
                    <IllustrationImage imageSrc={illustrationImageSrc} />
                  </IllustrationContainer>
                </Content>
              </Container>
            </AnimationRevealPage>
          )}
    </AuthContext.Consumer>
  );
}